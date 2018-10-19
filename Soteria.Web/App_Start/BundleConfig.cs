using System.Web;
using System.Web.Optimization;

namespace Soteria.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
						"~/Content/assets/global/plugins/jquery.min.js",
						"~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
						"~/Content/assets/global/plugins/js.cookie.min.js",
						"~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
						"~/Content/assets/global/plugins/jquery.blockui.min.js",
						"~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
						"~/Content/assets/global/plugins/moment.min.js",
						"~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js",
						"~/Content/assets/global/plugins/counterup/jquery.waypoints.min.js",
						"~/Content/assets/global/plugins/counterup/jquery.counterup.min.js",
						"~/Content/assets/global/plugins/morris/morris.min.js",
						"~/Content/assets/global/plugins/morris/raphael-min.js",
						"~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.js",
						"~/Content/assets/global/plugins/horizontal-timeline/horizontal-timeline.js",
						"~/Content/assets/global/plugins/flot/jquery.flot.min.js",
						"~/Content/assets/global/plugins/flot/jquery.flot.resize.min.js",
						"~/Content/assets/global/plugins/plugins/jquery.sparkline.min.js",
						"~/Content/assets/global/scripts/app.min.js",
						"~/Content/assets/pages/scripts/dashboard.min.js",
						"~/Content/assets/layouts/layout/scripts/layout.min.js",
						"~/Content/assets/layouts/layout/scripts/demo.min.js",
						"~/Content/assets/layouts/global/scripts/quick-sidebar.min.js",
						"~/Content/assets/layouts/global/scripts/quick-nav.min.js",
						"~/Scripts/app/global.js",
						"~/Scripts/app/dashboard.js"
			));

            bundles.Add(new StyleBundle("~/Content/css").Include(
						 "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
						 "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
						 "~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css",
						 "~/Content/assets/global/plugins/morris/morris.css",
						 "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.css",
						 "~/Content/assets/global/plugins/jqvmap/jqvmap/jqvmap.css",
						 "~/Content/assets/global/css/components.min.css",
						 "~/Content/assets/global/css/plugins.min.css",
						 "~/Content/assets/layouts/layout/css/layout.min.css",
						 "~/Content/assets/layouts/layout/css/themes/darkblue.min.css",
						 "~/Content/assets/layouts/layout/css/custom.min.css"
            ));
        }
    }
}
