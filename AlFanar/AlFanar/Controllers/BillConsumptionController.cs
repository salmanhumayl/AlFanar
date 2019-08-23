using Services.BillConsumption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Domain;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Services.Customer;
using AJC.KMS.Models;
using System.Configuration;
using CrystalDecisions.Shared;

namespace AlFanar.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class BillConsumptionController : Controller
    {
    
        private Services.BillConsumption.IBillConsumption _Service;
        private ICustomer _CustomerService;
        // GET: BillConsumption
        public ActionResult List()
        {
            _Service = new BillConsumptionService();


            var obj = _Service.GetListView();

            if (TempData["ErrorMsg"] != null)
            {
                string msg = Convert.ToString(TempData["ErrorMsg"]);
                ModelState.AddModelError("ErrorMsg", msg);
            }

            return View(obj);
        }

        public ActionResult BillTransaction()
        {

        _Service = new BillConsumptionService();
         var obj = _Service.GetListView();

        return PartialView("_BillTransaction", obj);
        }


        public ActionResult Add()
        {
           

            return View();
        }

        public ActionResult AddTEST()
        {


            return View();
        }
        public ActionResult Confirmation()
        {

            
            return View();
        }


        public ActionResult GetcustomerDetail(string ContractNo)
        {
            _Service = new BillConsumptionService();
            _CustomerService = new CustomerService();

            var ViewModel = new Services.BillConsumption.BillConsumptionAddViewModel
            {
                CustomerDetail = _CustomerService.GetCustomerDetail(ContractNo),
                ReadingDetail= _Service.GetLastReadingDetail(ContractNo),
                CustomerOutStanding= _Service.GetBillOutstanding(ContractNo),

            };
           
           // ViewModel.BillHeader.CurrentReadingDate = System.DateTime.Now.Date;
            return PartialView("_AddBillDetail", ViewModel);

        }


        [HttpPost]

        public ActionResult SaveBill(Services.BillConsumption.BillConsumptionAddViewModel model)
        {
            string ErrorMsg = "";
            _Service = new BillConsumptionService();
            int RecordID = _Service.Insert(model, ref ErrorMsg);

            if (ErrorMsg != "")
            {
                if (TempData["ErrorMsg"] == null)
                {
                    TempData["ErrorMsg"] = ErrorMsg;
                }
            }
            else if (RecordID > 0)
            {

                Stock.UpdateStockTankComsumption("IN", 0, RecordID, model.CustomerDetail.TankID, model.CustomerDetail.ContractNo, "tblBillConsumption", "ID", false);

                if (TempData["ErrorMsg"] == null)
                {
                    TempData["ErrorMsg"] = "Bill Created Added Successfully";
                }
            }

            TempData["BillID"] = RecordID;
            return RedirectToAction("Confirmation");

        }

      

        public ActionResult PrintBill(int id)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"),"rptBillConsumption.rpt"));

            Database crDatabase;
            Tables crTables;
            //Table crTable;
            TableLogOnInfo crTableLogonInfo;
            crDatabase = rd.Database;
            crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                  crTableLogonInfo = crTable.LogOnInfo;
                crTableLogonInfo.ConnectionInfo.ServerName = ConfigurationManager.AppSettings["ServerName"];
                crTableLogonInfo.ConnectionInfo.UserID = ConfigurationManager.AppSettings["DbuserName"];
                crTableLogonInfo.ConnectionInfo.Password = ConfigurationManager.AppSettings["DbPwd"];
                //crTableLogonInfo.ConnectionInfo
                crTable.ApplyLogOnInfo(crTableLogonInfo);


            }
            rd.SetDatabaseLogon(ConfigurationManager.AppSettings["DbuserName"], ConfigurationManager.AppSettings["DbPwd"], ConfigurationManager.AppSettings["ServerName"], "GasConsumption");
            rd.SetParameterValue("BillID", id);
            //rd.RecordSelectionFormula = "{tblBillConsumption.id}=" + id;
          
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Bill Consumption.pdf");

        }

    }
}