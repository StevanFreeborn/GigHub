using System.Data.Entity.Migrations;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Persistence;
using NUnit.Framework;

namespace GigHub.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [SetUp]
        public void SetUp()
        {
            MigrateDbToLatestVersion();
            Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any()) return;

            var user1 = new ApplicationUser
            {
                UserName = "user1",
                Name = "user1",
                Email = "-",
                PasswordHash = "-"
            };

            var user2 = new ApplicationUser
            {
                UserName = "user2",
                Name = "user2",
                Email = "-",
                PasswordHash = "-"
            };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.SaveChanges();
        }
    }
}
