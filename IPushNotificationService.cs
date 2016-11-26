using ACB.FCMPushNotifications.Data;
using ACB.FCMPushNotifications.Models;
using System.Threading.Tasks;

namespace ACB.FCMPushNotifications
{
    public interface IPushNotificationService
    {
        /// <summary>
        /// Send notification to users
        /// </summary>
        /// <param name="request">Define notification content and audience</param>
        /// <returns></returns>
        Task NotifyAsync(NotificationRequest request);

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
