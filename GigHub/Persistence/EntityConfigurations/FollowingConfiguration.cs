﻿using System.Data.Entity.ModelConfiguration;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class FollowingConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            HasKey(f => new {f.FollowerId, f.FolloweeId});
        }
    }
}