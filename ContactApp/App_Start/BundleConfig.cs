using System.Web;
using System.Web.Optimization;

           

namespace ContactApp
{
   
    public class BundleConfig
    {
        
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
 

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/ContactApp")
            .IncludeDirectory("~/Scripts/Controllers", "*.js")
            .Include("~/Scripts/ContactApp.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
