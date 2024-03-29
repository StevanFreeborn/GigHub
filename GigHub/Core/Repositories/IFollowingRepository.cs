﻿using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string artistId, string userId);
        IEnumerable<ApplicationUser> GetFollowees(string userId);
        void Add(Following following);
        void Remove(Following following);
    }
}