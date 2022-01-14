using System.Web.Optimization;

namespace GigHub
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/scripts/app/services/attendanceService.js",
                "~/scripts/app/services/followingsService.js",
                "~/scripts/app/controllers/gigsDetailsController.js",
                "~/scripts/app/controllers/gigsController.js",
                "~/scripts/app/controllers/notificationController.js",
                "~/scripts/app.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootbox.min.js",
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/underscore-umd-min.js",
                "~/Scripts/moment.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"
            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css",
                "~/Content/animate.css"
            ));
        }
    }
}