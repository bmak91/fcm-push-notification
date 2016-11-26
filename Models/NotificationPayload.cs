namespace ACB.FCMPushNotifications.Models
{
    internal class NotificationPayload
    {
        /// <summary>
        /// Indicates notification title. This field is not visible on iOS phones and tablets.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Indicates notification body text.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Indicates the action associated with a user click on the notification.
        /// <para>iOS: Corresponds to category in the APNs payload.</para>
        /// <para>Android: When this is set, an activity with a matching intent filter is launched when user clicks the notification.</para>
        /// </summary>
        public string ClickAction { get; set; }
    }
}