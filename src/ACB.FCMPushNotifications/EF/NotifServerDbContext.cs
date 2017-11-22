using ACB.FCMPushNotifications.Models;
using Microsoft.EntityFrameworkCore;

namespace ACB.FCMPushNotifications.EF
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDeviceToken>().HasKey(t => new { t.UserId, t.Token });
        }

        /// <summary>
        /// UserDeviceTokens Table
        /// </summary>
        public DbSet<UserDeviceToken> UserDeviceTokens { get; set; }
    }
}
