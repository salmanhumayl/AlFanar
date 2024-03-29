﻿using System.Web;
using System.Web.Optimization;

namespace AlFanar
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            
            //DatePicker Calandar 

           bundles.Add(new ScriptBundle("~/bundles/jqueryui")
              .Include("~/Scripts/jquery-ui-{version}.js"));


            //JQUERY DataTable For Export etc
            

            bundles.Add(new ScriptBundle("~/bundles/AJCExport").Include(
                        "~/Scripts/DataTables/AJCDataTables/jquery.dataTables.min.js",
                        "~/Scripts/DataTables/AJCDataTables/dataTables.buttons.min.js",
                        "~/Scripts/DataTables/AJCDataTables/jszip.min.js",
                        "~/Scripts/DataTables/AJCDataTables/pdfmake.min.js",
                        "~/Scripts/DataTables/AJCDataTables/vfs_fonts.js",
                        "~/Scripts/DataTables/AJCDataTables/buttons.html5.min.js",
                        "~/Scripts/DataTables/AJCDataTables/buttons.print.min.js"));
                        





            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/KMCSite.css",
                      "~/Content/site.css"
                      ));
        }
    }
}
