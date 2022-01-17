using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Moq;
using FluentAssertions;
using GigHub.Tests.Extensions;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendance>> _mockAttendances;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockAttendances = new Mock<DbSet<Attendance>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            mockContext.Setup(c => c.Attendances).Returns(_mockAttendances.Object);

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

        [TestMethod]
        public void GetUsersUpcomingGigs_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUsersUpcomingGigs("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUsersUpcomingGigs_GigIsForADifferentUser_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUsersUpcomingGigs(gig.ArtistId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUsersUpcomingGigs_GigIsForTheGivenUserAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUsersUpcomingGigs(gig.ArtistId);

            gigs.Should().Contain(gig);
        }

        [TestMethod]
        public void GetGigsUserAttending_GivenUserIsAttending_ShouldBeReturned()
        {
            var gig = new Gig() {DateTime = DateTime.Now.AddDays(1)};
            var attendance = new Attendance {Gig = gig, AttendeeId = "1"};

            _mockAttendances.SetSource(new [] {attendance});

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().Contain(gig);
        }

        [TestMethod]
        public void GetGigsUserAttending_AttendanceForADifferentUser_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1) };
            var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1) };
            var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsCanceled_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1),};
            var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            gig.Cancel();

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().Contain(gig);
        }

    }
}
