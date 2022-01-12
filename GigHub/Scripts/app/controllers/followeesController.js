var FolloweesController = function (followingsService) {

    var button;

    var fail = function () {
        alert("Something failed!");
    }

    var done = function () {
        var text = (button.text() === "Following") ? "Follow" : "Following";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

    var toggleFollow = function (e) {
        button = $(e.target);

        var followeeId = button.attr("data-user-id");

        if (button.hasClass("btn-default")) {
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