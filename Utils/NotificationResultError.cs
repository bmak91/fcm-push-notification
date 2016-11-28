namespace ACB.FCMPushNotifications.Utils
{
    /// <summary>
    /// Notification result error types
    /// </summary>
    public enum NotificationResultError
    {
        MissingRegistration,
        InvalidRegistration,
        NotRegistered,
        InvalidPackageName,
        MismatchSenderId,
        MessageTooBig,
        InvalidDataKey,
        InvalidTtl,
        Unavailable,
        InternalServerError,
        DeviceMessageRateExceeded
    }
}