namespace ACB.FCMPushNotifications.Utils
{
    /// <summary>
    /// Result object returned for each device notified
    /// </summary>
    public class NotificationResult
    {
        /// <summary>
        /// Id of user to whom the notification was sent
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Indicates whether the notification was sent successfully
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The type of error returned by FCM
        /// </summary>
        public NotificationResultError? Error { get; set; }
    }
}