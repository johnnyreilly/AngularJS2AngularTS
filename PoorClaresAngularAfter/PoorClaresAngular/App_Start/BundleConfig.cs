using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace PoorClaresAngular
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/poorClaresApp")
                .Include("~/content/site.css"));

            bundles.Add(new ScriptBundle("~/js/poorClaresApp").Include(
                
                // 3rd party libraries
                "~/scripts/jquery-{version}.js",
                "~/scripts/angular.js",
                "~/scripts/angular-route.js",
                "~/scripts/angular-animate.js",
                "~/scripts/angular-ui/ui-bootstrap-tpls.js",

                // App files
                "~/js/app.js")

                .IncludeDirectory("~/js/controllers", "*.js", false)
                .IncludeDirectory("~/js/services", "*.js", false)
                );
        }
    }
}