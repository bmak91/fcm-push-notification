using ACB.FCMPushNotifications.Data;
using ACB.FCMPushNotifications.Models;
using ACB.FCMPushNotifications.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ACB.FCMPushNotifications.Data.Abstractions;

namespace ACB.FCMPushNotifications
{
    /// <summary>
    /// Service class to send push notifications using FCM.
    /// </summary>
    public class FcmPushNotificationService : IFcmPushNotificationService
    {
        private HttpClient _Http { get; set; }
        private readonly IUserDeviceRepository _repo;

        private string FCMServerToken { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public FcmPushNotificationService(IOptions<PushNotificationServiceOptions> options,
                                          IUserDeviceRepository repo)
        {
            if (string.IsNullOrWhiteSpace(options.Value.FCMServerToken))
            {
                throw new Exception("FCM Server Key is required");
            }

            FCMServerToken = options.Value.FCMServerToken;

            _repo = repo;

            _Http = new HttpClient
            {
                BaseAddress = new Uri("https://fcm.googleapis.com/fcm/")
            };
            _Http.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={FCMServerToken}");
        }

        /// <summary>
        /// Send notification to users
        /// </summary>
        public async Task<List<NotificationResult>> NotifyAsync(NotificationRequest request)
        {
            var userTokensQuery = _repo.GetTokensByUserIds(request.UserIds);

            if (request.LimitByPlatform.HasValue)
            {
                userTokensQuery = userTokensQuery.Where(ut => ut.Platform == request.LimitByPlatform.Value);
            }

            var userTokens = userTokensQuery.ToList();

            if (userTokens.Count == 0)
            {
                return new List<NotificationResult>();
            }

            var ttl = request.TimeToLive?.TotalSeconds ?? TimeSpan.FromDays(28).TotalSeconds;
            var notification = new NotificationMessage
            {
                DryRun = request.DryRun,
                RegistrationIds = userTokens.Select(r => r.Token).ToList(),
                TimeToLive = Math.Max(0, ttl),
                Notification = new NotificationPayload
                {
                    Title = request.Title,
                    Body = request.Message
                },
                Data = request.Data
            };

            var jsonPayload = await Task.Run(() =>
                JsonConvert.SerializeObject(
                    notification,
                    Formatting.None,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new SnakeCasePropertyNameContractResolver()
                    }
                )
            );

            var payload = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _Http.PostAsync("send", payload);
            var content = await response.Content.ReadAsStringAsync();
            var json = await Task.Run(() => JsonConvert.DeserializeObject<FCMResponse>(content));

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:

                    var tasks = new List<Task>();
                    var results = new List<NotificationResult>();

                    for (var i = 0; i < json.Results.Count; i++)
                    {
                        var result = json.Results[i];
                        var userToken = userTokens[i];

                        results.Add(new NotificationResult
                        {
                            UserId = userToken.UserId,
                            Success = !result.Error.HasValue,
                            Error = result.Error
                        });

                        if (!string.IsNullOrWhiteSpace(result.RegistrationId))
                        {
                            tasks.Add(UnregisterUserAsync(userToken.UserId, userToken.Token));
                            tasks.Add(
                                RegisterUserAsync(
                                    userToken.UserId, result.RegistrationId, userToken.Platform
                                )
                            );
                        }
                        else if (result.Error.HasValue)
                        {
                            tasks.Add(HandleResponseError(userToken, result));
                        }
                    }

                    await Task.WhenAll(tasks.ToArray());
                    return results;

                case (HttpStatusCode)400:
                    throw new Exception("Invalid JSON");

                case (HttpStatusCode)401:
                    throw new Exception("Authentication Error");

                default:
                case (HttpStatusCode)500:
                    throw new Exception($"Internal Server Error: Multicast Id: {json?.MulticastId}");
            }
        }

        private Task HandleResponseError(UserDeviceToken userToken, FCMResponse.Result result)
        {
            switch (result.Error)
            {
                case NotificationResultError.InvalidRegistration:
                case NotificationResultError.NotRegistered:
                    return UnregisterUserAsync(userToken.UserId, userToken.Token);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Save user device token
        /// </summary>
        public async Task RegisterUserAsync(string userId, string userToken, DevicePlatform platform)
        {
            var isDuplicate = await _repo.GetTokenExistsAsync(userId, userToken);

            if (!isDuplicate)
            {
                await _repo.AddTokenAsync(new UserDeviceToken
                {
                    UserId = userId,
                    Token = userToken,
                    Platform = platform
                });
            }
        }

        /// <summary>
        /// Delete user device token
        /// </summary>
        public async Task UnregisterUserAsync(string userId, string userToken)
        {
            var userRegId = await _repo.GetTokenAsync(userId, userToken);
                
            if (userRegId != null)
            {
                await _repo.DeleteTokenAsync(userRegId);
            }
        }
    }
}
