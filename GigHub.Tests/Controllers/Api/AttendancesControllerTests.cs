﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Attend_AttendanceAlreadyExists_ShouldReturnBadRequest()
        {
            var attendance = new Attendance();

            _mockRepository.Setup(r => r.GetAttendance(1, _userId)).Returns(attendance);

            var result = _controller.Attend(new AttendanceDto {GigId = 1});

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_AttendanceDoesNotExist_ShouldReturnOk()
        {
            var result = _controller.Attend(new AttendanceDto {GigId = 1});

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void DeleteAttendance_NoAttendanceWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.DeleteAttendance(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnOk()
        {
            var attendance = new Attendance();

            _mockRepository.Setup(r => r.GetAttendance(1, _userId)).Returns(attendance);

            var result = _controller.DeleteAttendance(1);

            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnTheIdOfDeleteAttendance()
        {
            var attendance = new Attendance();

            _mockRepository.Setup(r => r.GetAttendance(1, _userId)).Returns(attendance);

            var result = (OkNegotiatedContentResult<int>) _controller.DeleteAttendance(1);

            result.Content.Should().Be(1);
        }

    }
}
