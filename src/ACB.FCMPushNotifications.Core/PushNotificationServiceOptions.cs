namespace ACB.FCMPushNotifications
{
    /// <summary>
    /// Configuration object for PushNotificationService
    /// </summary>
    public class PushNotificationServiceOptions
    {
        /// <summary>
        /// FCM server token found in the FCM console
        /// </summary>
        public string FCMServerToken { get; set; }
    }
}
