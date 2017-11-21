using Microsoft.EntityFrameworkCore;

namespace ACB.FCMPushNotifications.Data
{
    /// <summary>
    /// DbContext for PushNotificationService
    /// </summary>
    public class NotifServerDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NotifServerDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// UserDeviceTokens Table
        /// </summary>
        public DbSet<UserDeviceToken> UserDeviceTokens { get; set; }
    }
}
