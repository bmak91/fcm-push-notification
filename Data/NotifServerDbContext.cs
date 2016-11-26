using Microsoft.EntityFrameworkCore;

namespace ACB.FCMPushNotifications.Data
{
    public class NotifServerDbContext : DbContext
    {
        public NotifServerDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<UserDeviceToken> UserDeviceTokens { get; set; }
    }
}
