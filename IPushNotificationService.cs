using ACB.FCMPushNotifications.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using ACB.FCMPushNotifications.Utils;

namespace ACB.FCMPushNotifications
{
    /// <summary>
    /// Push Notification Service interface
    /// </summary>
    public interface IPushNotificationService
    {
        /// <summary>
        /// Send notification to users
        /// </summary>
        /// <param name="request">Define notification content and audience</param>
        /// <returns></returns>
        Task<List<NotificationResult>> NotifyAsync(NotificationRequest request);

        /// <summary>
        /// Save user device token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="regToken"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task RegisterUserAsync(string userId, string regToken, DevicePlatform platform);

        /// <summary>
        /// Delete user device token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="regToken"></param>
        /// <returns></returns>
        Task UnregisterUserAsync(string userId, string regToken);
    }
}
