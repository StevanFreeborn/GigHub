using GigHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FolloweesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Followees()
        {
            var followees = _unitOfWork.Followings.GetFollowings(User.Identity.GetUserId());

            return View(followees);
        }
    }
}