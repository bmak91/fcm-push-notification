namespace ACB.FCMPushNotifications.Models
{
    /// <summary>
    /// Notification result error types
    /// </summary>
    public enum NotificationResultError
    {
        /// <summary>
        /// Request missing a registration token.
        /// </summary>
        MissingRegistration,

        /// <summary>
        /// Registration token format is not valid.
        /// </summary>
        InvalidRegistration,

        /// <summary>
        /// Registration token no longer valid.
        /// </summary>
        NotRegistered,

        /// <summary>
        /// Message was addressed to a registration token whose package 
        /// name does not match the value passed in the request.
        /// </summary>
        InvalidPackageName,

        /// <summary>
        /// A registration token is tied to a certain group of senders. When a client app 
        /// registers for FCM, it must specify which senders are allowed to send messages. 
        /// You should use one of those sender IDs when sending messages to the client app. 
        /// If you switch to a different sender, the existing registration tokens won't work.
        /// </summary>
        MismatchSenderId,

        /// <summary>
        /// Check that the total size of the payload data included in a message does not 
        /// exceed FCM limits: 4096 bytes for most messages, or 2048 bytes in the case of 
        /// messages to topics or notification messages on iOS. This includes both the keys 
        /// and the values.
        /// </summary>
        MessageTooBig,

        /// <summary>
        /// Check that the payload data does not contain a key (such as from, or gcm, 
        /// or any value prefixed by google) that is used internally by FCM. 
        /// </summary>
        InvalidDataKey,

        /// <summary>
        /// Check that the value used in time_to_live is an integer representing 
        /// a duration in seconds between 0 and 2,419,200 (4 weeks).
        /// </summary>
        InvalidTtl,

        /// <summary>
        /// The server couldn't process the request in time.
        /// </summary>
        Unavailable,

        /// <summary>
        /// The server encountered an error while trying to process the request.
        /// </summary>
        InternalServerError,

        /// <summary>
        /// The rate of messages to a particular device is too high. 
        /// Reduce the number of messages sent to this device and do not immediately 
        /// retry sending to this device.
        /// </summary>
        DeviceMessageRateExceeded
    }
}