using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence.EntityConfigurations;

namespace GigHub.Tests.Core.Models
{
    [TestClass]
    public class GigTests
    {
        [TestMethod]
        public void Cancel_WhenCalled_ShouldSetIsCanceledToTrue()
        {
            var gig = new Gig();
            gig.Cancel();
            gig.IsCanceled.Should().BeTrue();
        }

        [TestMethod]
        public void Cancel_WhenCalled_EachAttendeeShouldHaveANotification()
        {
            var gig = new Gig();
            gig.Attendances.Add(new Attendance {Attendee = new ApplicationUser { Id = "1"}});
            gig.Attendances.Add(new Attendance { Attendee = new ApplicationUser { Id = "2" } });

            gig.Cancel();

            var attendees = gig.GetAttendees();

            foreach (var attendee in attendees)
            {
                attendee.UserNotifications.Count.Should().Be(1);
            }
        }
    }
}
