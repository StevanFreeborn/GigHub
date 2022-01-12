var GigsDetailsController = function (followingsService) {

    var followButton;

    var fail = function () {
        alert("Something failed!");
    }

    var done = function () {
        var text = (followButton.text().trim() === "Follow") ? "Following" : "Follow";

        followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

    var toggleFollow = function (e) {
        followButton = $(e.target);

        var followeeId = followButton.attr("data-user-id");

        if (followButton.hasClass("btn-default")) {
            followingsService.createFollowing(followeeId, done, fail);
        } else {
            followingsService.deleteFollowing(followeeId, done, fail);
        }
    }

    var init = function (container) {
        $(container).on("click", ".js-toggle-follow", toggleFollow);
    };

    return {
        init: init
    }

}(FollowingsService);