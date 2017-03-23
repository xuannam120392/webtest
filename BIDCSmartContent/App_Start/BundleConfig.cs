using System.Web;
using System.Web.Optimization;

namespace BIDVSmartContent
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/theme.css",
                      "~/Content/jquery-ui.min.css",
                      "~/Content/skins.min.css",
                      "~/Content/rtl.min.css",
                      "~/Content/font-awesome.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/other").Include(
                "~/Scripts/jquery-ui.min.js",
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/jquery.dataTables.bootstrap.js",
                "~/Scripts/sms.min.js",
                "~/Scripts/elements.min.js",
                "~/Scripts/sms-extra.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
               "~/Scripts/angular.min.js",
               "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js"
              ));
        }
    }
}