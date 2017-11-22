using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACB.FCMPushNotifications.Models;

namespace ACB.FCMPushNotifications.Data.Abstractions
{
    /// <summary>
    /// User device repository. Implement this interface with your preferred ORM.
    /// </summary>
    public interface IUserDeviceRepository
    {
        /// <summary>
        /// Adds the token.
        /// </summary>
        /// <returns>The token async.</returns>
        /// <param name="token">Token.</param>
        Task AddTokenAsync(UserDeviceToken token);

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <returns>The token async.</returns>
        /// <param name="userId">User identifier.</param>
        /// <param name="userToken">User token.</param>
        Task<UserDeviceToken> GetTokenAsync(string userId, string userToken);

        /// <summary>
        /// Gets the token exists.
        /// </summary>
        /// <returns>The token exists async.</returns>
        /// <param name="userId">User identifier.</param>
        /// <param name="userToken">User token.</param>
        Task<bool> GetTokenExistsAsync(string userId, string userToken);

        /// <summary>
        /// Gets the tokens by user identifiers.
        /// </summary>
        /// <returns>The tokens by user identifiers.</returns>
        /// <param name="userIds">User identifiers.</param>
        IQueryable<UserDeviceToken> GetTokensByUserIds(IEnumerable<string> userIds);

        /// <summary>
        /// Deletes the token.
        /// </summary>
        /// <returns>The token async.</returns>
        /// <param name="token">Token.</param>
        Task DeleteTokenAsync(UserDeviceToken token);
    }
}
