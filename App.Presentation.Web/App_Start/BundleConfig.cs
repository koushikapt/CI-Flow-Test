using System.Web.Optimization;

namespace App.Presentation.Web
{
    /// <summary>
    /// Configure the script and styleheet bundles.
    /// </summary>
    public static class BundleConfig
    {
        /// <summary>
        /// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862.
        /// Set EnableOptimizations to false for debugging. For more information, visit http://go.microsoft.com/fwlink/?LinkId=301862.
        /// </summary>
        public static void RegisterBundles(BundleCollection bundles)
        {
            if (bundles == null)
            {
                return;
            }

            bundles.Add(new ScriptBundle("~/Scripts/app").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/Site.css"));
        }
    }
}