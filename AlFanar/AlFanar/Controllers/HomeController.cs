using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AJC.KMS.Models;
using System.Drawing.Printing;

namespace AJC.KMS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {


        public ActionResult Index()
        {


          

            PrintDocument printDoc = new PrintDocument();

            printDoc.DocumentName = "Print Document";

           

            //Call ShowDialog

            //    printDoc.Print();
            Ticket obj = new Ticket(12, System.DateTime.Now.Date, "dsadas", "sadasd",1000,"salman");

            obj.print();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}