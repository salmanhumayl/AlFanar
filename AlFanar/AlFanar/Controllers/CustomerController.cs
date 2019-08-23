using AJC.KMS.Models;
using Services.Building;
using Services.Customer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AJC.KMS.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class CustomerController : Controller
    {
        // GET: Supplier
        private ICustomer _CustomerService;
        private IBuilding _BuildingService;

        public ActionResult List()
        {
            if (TempData["ErrorMsg"] != null)
            {
                string msg = Convert.ToString(TempData["ErrorMsg"]);
                ModelState.AddModelError("ErrorMsg", msg);
            }

            _CustomerService = new CustomerService();
            var OBJ = _CustomerService.GetListView();
            return View(OBJ);
           
        }

        public ActionResult Add()
        {

            _BuildingService = new BuildingService();
            ViewBag.Building = new SelectList(_BuildingService.GetList(), "ID", "BuildingNo");
            return View();

        }
        [HttpPost]
        public ActionResult Add(Core.Domain.Customer model)
        {
            string ErrorMsg = "";
            _CustomerService = new CustomerService();
           
            int ID = _CustomerService.Insert(model, ref ErrorMsg);

            if (ErrorMsg != "")
            {
                if (TempData["ErrorMsg"] == null)
                {
                    TempData["ErrorMsg"] = ErrorMsg;
                }
            }
            else if (ID > 0)
            {
                if (TempData["ErrorMsg"] == null)
                {
                    TempData["ErrorMsg"] = model.CustomerName + "  Added Successfully";
                }
            }
            return RedirectToAction("List");
            //  }



        }



    }
}