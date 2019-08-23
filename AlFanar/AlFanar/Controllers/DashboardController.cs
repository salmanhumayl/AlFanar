using AJC.KMS.Models;
using Core.Domain.Quotation;
using Services.Purchase_Requisition;
using Services.Quotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AJC.KMS.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class DashboardController : Controller
    {
        // GET: Dashboard
     
        public ActionResult Dashboard()
        {
           
            return View();
        }

        public ActionResult GetStockValue()
        {
            return PartialView("_TotalStockValue");
        }

    }
}