using ACB.FCMPushNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PushNotificationServiceCollectionExtensions
    {
        public static void AddFCMPushNotificationService(this IServiceCollection services, Action<PushNotificationServiceOptions> configure)
        {
            services.AddScoped<IPushNotificationService, FCMPushNotificationService>()
                .Configure(configure);
        }
    }
}
