using AJC.KMS.Models;
using Core.Domain.OpeningBalance;
using Core.Domain.StockAdjustment;
using Services.Tank;
using Services.Category;
using Services.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AlFanar.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class BuildingController : Controller
    {
        // GET: Item

       
        private Services.Building.IBuilding _IBuildingService;
        private Services.Tank.ITank _ITankService;

        public ActionResult Test()
        {
            
            return View("View");
        }

   
        public ActionResult Add()
        {
            _ITankService = new TankService();
            ViewBag.Tank = new SelectList(_ITankService.GetList(), "ID", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Add(Core.Domain.Building model)
        {
            string ErrorMsg = "";
            _IBuildingService = new BuildingService();
            //   if (ModelState.IsValid == true) // Server side validation
            //  {
            int ID = _IBuildingService.InsertBuilding(model, ref ErrorMsg);

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
                        TempData["ErrorMsg"] = model.BuildingName + "  Added Successfully";
                    }
                }
                return RedirectToAction("List");
          //  }
            
          

        }

        public ActionResult List()
        {

            _IBuildingService = new BuildingService();
            var obj = _IBuildingService.GetListView();

            if (TempData["ErrorMsg"] != null)
            {
                string msg = Convert.ToString(TempData["ErrorMsg"]);
                ModelState.AddModelError("ErrorMsg", msg);
            }

            return View(obj);
        }

        

      
       
       

    }
}