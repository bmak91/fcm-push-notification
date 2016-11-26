using System.ComponentModel.DataAnnotations;

namespace ACB.FCMPushNotifications.Data
{
    public class UserDeviceToken
    {
        public string UserId { get; set; }

        [Key]
        public string Token { get; set; }

        public DevicePlatform Platform { get; set; }
    }

    public enum DevicePlatform
    {
        Android,
        iOS
    }
}
