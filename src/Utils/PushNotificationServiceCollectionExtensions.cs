using ACB.FCMPushNotifications;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for FCMPushNotificationService
    /// </summary>
    public static class PushNotificationServiceCollectionExtensions
    {
        /// <summary>
        /// Register and configure scoped FCMPushNotificationService class. 
        /// Inject using IPushNotificationService interface.
        /// </summary>
        public static void AddFCMPushNotificationService(this IServiceCollection services, Action<PushNotificationServiceOptions> configure)
        {
            services.AddScoped<IPushNotificationService, FCMPushNotificationService>()
                .Configure(configure);
        }
    }
}
