using AJC.KMS.Models;
using Services.RefillTank;
using Services.Tank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AJC.KMS.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class ReFillTankController : Controller
    {
        // GET: Unit
        private IRefillTank _RefillService;
        private ITank _TankService;
        // GET: Tank
        public ActionResult Index()
        {
            _RefillService = new RefillTankService();
            var Unit = _RefillService.GetList();
            return View(Unit);
        }


        public ActionResult Add()
        {
            _TankService = new TankService();
            ViewBag.Tank= new SelectList(_TankService.GetList(), "ID", "Name");
            if (TempData["ConfimationMsg"] != null)
            {
                string msg = Convert.ToString(TempData["ConfimationMsg"]);
                ModelState.AddModelError("ConfimationMsg", msg);
            }
            return View();
            
        }
        [HttpPost]
        public ActionResult Add(Core.Domain.RefillTank model)
        {
            _RefillService = new RefillTankService();
            int RecordID = _RefillService.Insert(model);

            if (RecordID > 0)
            {
                  Stock.UpdateStockTank("DC", 1, RecordID, model.TankId,"tblRefillTank", "ID",false);
                if (TempData["ConfimationMsg"] == null)
                {
                    TempData["ConfimationMsg"] = "Tank Refill Successfully";
                }

            }
            return RedirectToAction("Add");
        }
    }
}