using System.Web.Optimization;

namespace İlkProjeWebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            // Bootstrap JS
            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.bundle.min.js"));

            // Site CSS
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.min.css", "~/Content/site.css"));

            // vs. ihtiyaç duyduğun diğer bundle’lar
        }
    }
}
