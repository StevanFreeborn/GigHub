using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Controllers.Extensions;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class FollowingsControllerTests
    {

        private FollowingsController _controller;
        private Mock<IFollowingRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IFollowingRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Followings).Returns(_mockRepository.Object);

            _controller = new FollowingsController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Follow_FollowingAlreadyExists_ShouldReturnBadRequest()
        {
            var following = new Following();

            _mockRepository.Setup(r => r.GetFollowing("1", _userId)).Returns(following);

            var result = _controller.Follow(new FollowingDto() { FolloweeId = "1" });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Follow_FollowingDoesNotExist_ShouldReturnBadRequest()
        {

            var result = _controller.Follow(new FollowingDto() { FolloweeId = "1" });

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void Unfollow_NoFollowingWithGivenIdExists_ShouldReturnBadRequest()
        {

            var result = _controller.Unfollow("1");

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Unfollow_ValidRequest_ShouldReturnOk()
        {
            var following = new Following();

            _mockRepository.Setup(r => r.GetFollowing("1", _userId)).Returns(following);

            var result = _controller.Unfollow("1");

            result.Should().BeOfType<OkNegotiatedContentResult<string>>();
        }

        [TestMethod]
        public void Unfollow_ValidRequest_ShouldReturnIdOfUnfollowedFollowing()
        {
            var following = new Following();

            _mockRepository.Setup(r => r.GetFollowing("1", _userId)).Returns(following);

            var result = (OkNegotiatedContentResult<string>)_controller.Unfollow("1");

            result.Content.Should().Be("1");
        }
    }
}
