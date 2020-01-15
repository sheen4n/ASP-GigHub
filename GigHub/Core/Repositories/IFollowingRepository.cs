using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string gigArtistId);
        void Add(Following following);
        void Delete(Following following);
        IEnumerable<ApplicationUser> GetFollowees(string userId);
    }
}