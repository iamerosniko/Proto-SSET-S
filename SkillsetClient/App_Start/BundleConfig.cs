﻿using System.Web.Optimization;

namespace SkillsetClient
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Scripts/styles.bundle.css"));


            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                        "~/Scripts/inline.bundle.js",
                        "~/Scripts/polyfills.bundle.js",
                        "~/Scripts/scripts.bundle.js",
                        "~/Scripts/main.bundle.js"));
        }
    }
}
