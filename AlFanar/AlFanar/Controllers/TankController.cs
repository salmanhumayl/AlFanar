using AJC.KMS.Models;
using Services.Tank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlFanar.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class TankController : Controller
    {
        private ITank _TankService;
        // GET: Tank
        public ActionResult Index()
        {
            _TankService = new TankService();
            var Category = _TankService.GetList();

            return View(Category);
        }

       
         public ActionResult GetTankStatistics()
        {
            _TankService = new TankService();
            var obj=_TankService.GetTotalConsumption();
            return PartialView("_TankMenu", obj);
        }


        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {

            _TankService = new TankService();
            var obj = _TankService.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(Core.Domain.Tank Model)
        {
            _TankService = new TankService();
            _TankService.Update(Model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add(Core.Domain.Tank model)
        {
            _TankService = new TankService();
            _TankService.InsertTank(model);
            return RedirectToAction("Index");
        }
    }
}