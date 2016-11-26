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

namespace ACB.FCMPushNotifications
{
    public class FCMPushNotificationService : IPushNotificationService
    {
        private HttpClient _Http { get; set; }
        private NotifServerDbContext _db { get; set; }

        private string FCMServerToken { get; set; }

        public FCMPushNotificationService(IOptions<PushNotificationServiceOptions> options, NotifServerDbContext db)
        {
            if (string.IsNullOrWhiteSpace(options.Value.FCMServerToken))
            {
                throw new Exception("FCM Server Key is required");
            }

            FCMServerToken = options.Value.FCMServerToken;

            _db = db;

            _Http = new HttpClient
            {
                BaseAddress = new Uri("https://fcm.googleapis.com/fcm/")
            };
            _Http.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={FCMServerToken}");
        }

        public async Task NotifyAsync(NotificationRequest request)
        {
            var userTokens = _db.UserDeviceTokens.Where(ut => request.UserIds.Contains(ut.UserId))
                                                .ToList();

            if (userTokens.Count == 0) return;

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

            var jsonPayload = JsonConvert.SerializeObject(notification, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new SnakeCasePropertyNameContractResolver()
            });

            var payload = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _Http.PostAsync("send", payload);
            var content = await response.Content.ReadAsStringAsync();
            var json = await Task.Run(() => JsonConvert.DeserializeObject<FCMResponse>(content));

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    if(json.CanonicalIds > 0 || json.Failure > 0)
                    {
                        var Tasks = new List<Task>();
                        for (var i = 0; i < json.Results.Count; i++)
                        {
                            var result = json.Results[i];
                            var userToken = userTokens[i];
                            if (!string.IsNullOrWhiteSpace(result.RegistrationId))
                            {                                
                                Tasks.Add(UnregisterUserAsync(userToken.UserId, userToken.Token));
                                Tasks.Add(RegisterUserAsync(userToken.UserId, result.RegistrationId, userToken.Platform));
                            }
                            else if(result.Error.HasValue)
                            {
                                Tasks.Add(HandleResponseError(userToken, result));
                            }
                        }

                        Task.WaitAll(Tasks.ToArray());
                        foreach(var t in Tasks)
                        {
                            if(t.IsFaulted)
                            {
                                throw t.Exception;
                            }
                        }
                    }
                    break;

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
            switch(result.Error)
            {
                case FCMResponse.ResultError.InvalidRegistration:
                case FCMResponse.ResultError.NotRegistered:
                    return UnregisterUserAsync(userToken.UserId, userToken.Token);

                // TODO: Implement handling of below errors
                case FCMResponse.ResultError.InvalidDataKey:
                case FCMResponse.ResultError.DeviceMessageRateExceeded:
                case FCMResponse.ResultError.MessageTooBig:
                    throw new Exception(result.Error.ToString());
            }
            return Task.CompletedTask;
        }

        public async Task RegisterUserAsync(string userId, string userToken, DevicePlatform platform)
        {
            _db.UserDeviceTokens.Add(new UserDeviceToken
            {
                UserId = userId,
                Token = userToken,
                Platform = platform
            });

            await _db.SaveChangesAsync();
        }

        public async Task UnregisterUserAsync(string userId, string userToken)
        {
            var userRegId = _db.UserDeviceTokens.FirstOrDefault(ur => ur.UserId == userId && ur.Token == userToken);
            if (userRegId != null)
            {
                _db.UserDeviceTokens.Remove(userRegId);
                await _db.SaveChangesAsync();
            }
        }
    }
}
