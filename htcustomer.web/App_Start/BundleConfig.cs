using System.Web;
using System.Web.Optimization;

namespace htcustomer.web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
            "~/Scripts/jquery-ui-1.12.1.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/toastr.css",
                      "~/Content/MyCss/BaseGoogle.css",
                      "~/Content/site.css"));

            // Device Bundle
            bundles.Add(new StyleBundle("~/Content/css/device").Include(
                "~/Content/MyCss/device.css"));
            bundles.Add(new ScriptBundle("~/Scripts/js/device").Include(
                "~/Scripts/MyScript/device/device.js"));

            // Contact Bundle 
            bundles.Add(new StyleBundle("~/Content/css/contact").Include(
                "~/Content/MyCss/contact.css"));
        }
    }
}
