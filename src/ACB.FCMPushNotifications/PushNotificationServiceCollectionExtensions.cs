using ACB.FCMPushNotifications;
using System;
using ACB.FCMPushNotifications.Data.Abstractions;
using ACB.FCMPushNotifications.EF;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for FCMPushNotificationService
    /// </summary>
    public static class PushNotificationServiceCollectionExtensions
    {
        /// <summary>
        /// Register and configure scoped FCMPushNotificationService class. 
        /// Inject using IFcmPushNotificationService interface.
        /// </summary>
        public static void AddFCMPushNotificationService(this IServiceCollection services, Action<PushNotificationServiceOptions> configure)
        {
            services.AddScoped<IUserDeviceRepository, UserDeviceRepository>();
            services.AddScoped<IFcmPushNotificationService, FcmPushNotificationService>()
                .Configure(configure);
        }
    }
}
