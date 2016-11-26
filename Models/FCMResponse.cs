using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACB.FCMPushNotifications.Models
{
    public class FCMResponse
    {
        /// <summary>
        /// Unique ID (number) identifying the multicast message.
        /// </summary>
        public int MulticastId { get; set; }

        /// <summary>
        /// Number of messages that were processed without an error.
        /// </summary>
        public int Success { get; set; }

        /// <summary>
        /// Number of messages that could not be processed.
        /// </summary>
        public int Failure { get; set; }

        /// <summary>
        /// Number of results that contain a canonical registration token. 
        /// A canonical registration ID is the registration token of the last registration requested by the client app. 
        /// This is the ID that the server should use when sending messages to the device.
        /// </summary>
        public int CanonicalIds { get; set; }

        /// <summary>
        /// Array of objects representing the status of the messages processed. The objects are listed in the same order as the request
        /// </summary>
        public List<Result> Results { get; set; }

        public class Result
        {
            /// <summary>
            /// String specifying a unique ID for each successfully processed message.
            /// </summary>
            public string MessageId { get; set; }

            /// <summary>
            /// Optional string specifying the canonical registration token for the client app that the message was processed and sent to. 
            /// Sender should use this value as the registration token for future requests. Otherwise, the messages might be rejected.
            /// </summary>
            public string RegistrationId { get; set; }

            /// <summary>
            /// String specifying the error that occurred when processing the message for the recipient.
            /// </summary>
            public ResultError? Error { get; set; }
        }

        public enum ResultError
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
}
