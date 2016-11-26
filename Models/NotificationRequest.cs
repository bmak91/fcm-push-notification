using System;
using System.Collections.Generic;

namespace ACB.FCMPushNotifications.Models
{
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
        /// Ids of users to target
        /// </summary>
        public List<string> UserIds { get; set; }

        /// <summary>
        /// Specifies the custom key-value pairs of the message's payload.
        /// </summary>
        public Dictionary<string, string> Data { get; set; }
    }
}
