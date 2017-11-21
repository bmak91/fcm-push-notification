using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACB.FCMPushNotifications.Data.Abstractions
{
    public interface IUserDeviceRepository
    {
        Task AddTokenAsync(UserDeviceToken token);
        Task<UserDeviceToken> GetTokenAsync(string userId, string userToken);
        Task<bool> GetTokenExistsAsync(string userId, string userToken);
        IQueryable<UserDeviceToken> GetTokensByUserIds(IEnumerable<string> userIds);
        Task DeleteTokenAsync(UserDeviceToken token);
    }
}
