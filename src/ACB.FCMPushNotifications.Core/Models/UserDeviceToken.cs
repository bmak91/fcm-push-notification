﻿
namespace ACB.FCMPushNotifications.Models
{
    /// <summary>
    /// Registration token of user device 
    /// </summary>
    public class UserDeviceToken
    {
        /// <summary>
        /// User Id 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Registration token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Device platform. <seealso cref="ACB.FCMPushNotifications.Models.DevicePlatform" >
        /// See DevicePlatform for supported platforms.</seealso>
        /// </summary>
        public DevicePlatform Platform { get; set; }
    }

    /// <summary>
    /// Device platform
    /// </summary>
    public enum DevicePlatform
    {
        /// <summary>
        /// Android
        /// </summary>
        Android,
        /// <summary>
        /// iOS
        /// </summary>
        iOS
    }
}