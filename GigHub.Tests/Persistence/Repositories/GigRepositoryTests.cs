using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Moq;
using System.Net.NetworkInformation;
using FluentAssertions;
using GigHub.Tests.Extensions;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);

            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUsersUpcomingGigs_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() {DateTime = DateTime.Now.AddDays(-1), ArtistId = "1"};

            _mockGigs.SetSource(new[] {gig});

            var gigs = _repository.GetUsersUpcomingGigs("1");

            gigs.Should().BeEmpty();
        }
    }
}
