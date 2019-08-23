using AJC.KMS.Models;
using Services.Building;
using Services.Category;

using Services.Helper;
using Services.Tank;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AJC.KMS.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class StockController : Controller
    {
        private Services.Building.IBuilding _BuildingService;
        private Services.Tank.ITank _TankService;
        
        private Services.Category.ICategory _CategoryService;
        public ActionResult StockAsOf()
        {
            _CategoryService = new CategoryService();
            
            List<AsofStock> obj;
            string categoryID = Request.Form["cmbCategory"];
            if (categoryID != "")
            {

                Int16 ID = Convert.ToInt16(Request.Form["cmbCategory"]);
                ViewBag.Category = new SelectList(_CategoryService.GetList(), "ID", "Name",ID);
                obj = Stock.GetAsofStock(ID);
            }
            else
            {
                ViewBag.Category = new SelectList(_CategoryService.GetList(), "ID", "Name");
                obj = Stock.GetAsofStock(0);
            }
            
            return View(obj);
        }


        public ActionResult StockLedgerTracking()
        {
            _TankService = new TankService();
            ViewBag.Tank = new SelectList(_TankService.GetList(), "ID", "Name");
            return View("StockLedger");
        }

        public ActionResult StockLedger(DateTime txtFromDate, DateTime txtToDate, int cmbBuilding)
        {
            List<StockLedger> obj;
            
            {
                
                obj = Stock.GetStockLedger(cmbBuilding);
            }

            return PartialView("_StockLedger",obj);
        }


       
        

        [HttpPost]
        public ActionResult DailyIssuance(DateTime txtFromDate, DateTime txtToDate, int cmbChief = 0, int cmbCategory = 0)
        {
            List<DailyIssuance> obj;

            {

                obj = Stock.GetDailyIssuance(txtFromDate, txtToDate, cmbChief, cmbCategory);
            }
            return PartialView("_DailyIssuance", obj);
        }


        [HttpPost]
        public ActionResult IssueReturn(DateTime txtFromDate, DateTime txtToDate)
        {
            List<PurchaseReturn> obj;

            {

                obj = Stock.GetIssueReturn(txtFromDate, txtToDate);
            }
            return PartialView("_IssueReturn", obj);
        }


        [HttpPost]
        public ActionResult PurchaseReturn(DateTime txtFromDate, DateTime txtToDate)
        {
            List<PurchaseReturn> obj;

            {

                obj = Stock.GetPurchaseReturn(txtFromDate, txtToDate);
            }
            return PartialView("_PurchaseReturn", obj);
        }


        [HttpPost]
        public ActionResult DailyPurchase(DateTime txtFromDate, DateTime txtToDate,string InvoiceNo)
        {
            List<DailyPurchase> obj;

            {

                obj = Stock.GetDailyPurchase(txtFromDate, txtToDate, InvoiceNo);
            }
            return PartialView("_DailyPurchase", obj);
        }


        public ActionResult DayEndTracking()
        {

            return View("DayEndSelection");
        }


        public ActionResult DayEnd()
        {
            List<DayEndReport> obj;
            _CategoryService = new CategoryService();

            string categoryID = Request.Form["cmbCategory"];
            if (categoryID != "")
            {
                Int16 ID = Convert.ToInt16(Request.Form["cmbCategory"]);
                ViewBag.Category = new SelectList(_CategoryService.GetList(), "ID", "Name", ID);
                obj = Stock.GetDayEndReport(ID);
            }
            else
            {
                ViewBag.Category = new SelectList(_CategoryService.GetList(), "ID", "Name");
                obj = Stock.GetDayEndReport(0);

            }

            return View("_DayEnd", obj);
        }

       // [HandleError]
        public ActionResult GetStockValue()
        {
            var obj=Stock.GetStockValue(1);
            return PartialView("_TotalStockValue", obj);
        }

        public ActionResult GetStockValueCategoryWise()
        {
            var obj = Stock.GetStockValueCategoryWise(1);
            return PartialView("_TotalStockValueCategoryWise", obj);
        }
        public ActionResult GetConsumptionCategoryWise()
        {
            var obj = Stock.GetConsumptionCategoryWise(1);
            return PartialView("_TotalConsumptionCategoryWise", obj);
        }


        public ActionResult GetIssueValue()
        {
            var obj = Stock.GetIssueValue(1);
            return PartialView("_TotalIssueValue", obj);
        }


        public ActionResult GetPurchaseValue()
        {
            var obj = Stock.GetPurchaseValue(1);
            return PartialView("_TotalPurchaseValue", obj);
        }


        public ActionResult GetIssueReturnValue()
        {
            var obj = Stock.GetIssueReturnValue(1);//Kitchen ID
            return PartialView("_TotalIssueReturnValue", obj);
        }

        public ActionResult GetPurchaseReturnValue()
        {
            var obj = Stock.GetPurchaseReturnValue(1);//Kitchen ID
            return PartialView("_TotalPurchaseReturnValue", obj);
        }

        public ActionResult TransactionPanel()
        {
            var obj = Stock.TransactionPanel(1);//Kitchen ID
            return PartialView("_TransactionPanel", obj);
        }
        
    }
}