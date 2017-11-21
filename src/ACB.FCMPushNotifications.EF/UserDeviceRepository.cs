using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACB.FCMPushNotifications.Data.Abstractions;
using ACB.FCMPushNotifications.Models;

namespace ACB.FCMPushNotifications.EF
{
    public class UserDeviceRepository : IUserDeviceRepository
    {
        private readonly NotifServerDbContext _db;

        public UserDeviceRepository(NotifServerDbContext context)
        {
            _db = context;
        }

        public Task AddTokenAsync(UserDeviceToken token)
        {
            _db.UserDeviceTokens.Add(token);
            return _db.SaveChangesAsync();
        }

        public Task DeleteTokenAsync(UserDeviceToken token)
        {
            _db.UserDeviceTokens.Remove(token);
            return _db.SaveChangesAsync();
        }

        public Task<UserDeviceToken> GetTokenAsync(string userId, string userToken)
        {
            var token = 
                _db.UserDeviceTokens
                   .FirstOrDefault(ur => ur.UserId == userId
                                      && ur.Token == userToken);

            return Task.FromResult(token);
        }

        public Task<bool> GetTokenExistsAsync(string userId, string userToken)
        {
            var isDuplicate =
                _db.UserDeviceTokens
                   .Any(ur => ur.UserId == userId
                           && ur.Token == userToken);

            return Task.FromResult(isDuplicate);
        }

        public IQueryable<UserDeviceToken> GetTokensByUserIds(IEnumerable<string> userIds)
        {
            return _db.UserDeviceTokens.Where(ut => userIds.Contains(ut.UserId));
        }
    }
}
