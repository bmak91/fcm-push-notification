using System;
using System.Collections.Generic;

namespace ACB.FCMPushNotifications.Models
{
    internal class NotificationMessage
    {
        public bool DryRun { get; set; }

        /// <summary>
        /// This parameter specifies the recipient of a message.
        /// <para>The value must be a registration token, notification key, or topic.
        /// Do not set this field when sending to multiple topics.</para>
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// This parameter specifies a list of devices (registration tokens, or IDs) receiving a multicast message. 
        /// It must contain at least 1 and at most 1000 registration tokens.
        /// <para>Use this parameter only for multicast messaging, not for single recipients. 
        /// Multicast messages (sending to more than 1 registration tokens) are allowed using HTTP JSON format only.</para>
        /// </summary>
        public List<string> RegistrationIds { get; set; }

        /// <summary>
        /// On iOS, use this field to represent content-available in the APNs payload. 
        /// When a notification or message is sent and this is set to true, an inactive client app is awoken. 
        /// On Android, data messages wake the app by default.
        /// </summary>
        public bool ContentAvailable { get; set; }

        /// <summary>
        /// This parameter specifies how long (in seconds) the message should be kept in FCM storage if the device is offline. 
        /// The maximum time to live supported is 4 weeks, and the default value is 4 weeks.
        /// </summary>
        public double TimeToLive { get; set; }

        /// <summary>
        /// This parameter specifies the predefined, user-visible key-value pairs of the notification payload. 
        /// </summary>
        public NotificationPayload Notification { get; set; }

        /// <summary>
        /// This parameter specifies the custom key-value pairs of the message's payload.
        /// </summary>
        public Dictionary<string,string> Data { get; set; }
    }
}
