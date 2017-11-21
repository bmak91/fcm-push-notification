using System;
using System.Collections.Generic;
using ACB.FCMPushNotifications.Data;

namespace ACB.FCMPushNotifications.Utils
{
    /// <summary>
    /// Describes the notification to be sent. 
    /// </summary>
    public class NotificationRequest
    {
        /// <summary>
        /// Indicates notification title. This field is not visible on iOS phones and tablets.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Indicates notification body text.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Specifies how long (in seconds) the message should be kept in FCM storage if the device is offline. The maximum time to live supported is 4 weeks.
        /// </summary>
        public TimeSpan? TimeToLive { get; set; }

        /// <summary>
        /// Set to true to test a request without actually sending a message.
        /// </summary>
        public bool DryRun { get; set; }

        /// <summary>
        /// (Optional) Target devices on a single platform.
        /// </summary>
        public DevicePlatform? LimitByPlatform { get; set; }

        /// <summary>
        /// Ids of users to target
        /// </summary>
        public List<string> UserIds { get; set; }

        /// <summary>
        /// Specifies the custom key-value pairs of the message's payload.
        /// </summary>
        public Dictionary<string, string> Data { get; set; }
    }
}
