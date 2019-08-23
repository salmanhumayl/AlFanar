using AJC.KMS.Models;
using Services.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AJC.KMS.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class CategoryController : Controller
    {
        // GET: Category

        private ICategory _CategoryService;
        public ActionResult Index()
        {
            _CategoryService = new CategoryService();
            var Category = _CategoryService.GetList();

            return View(Category);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Core.Domain.Category model)
        {
            _CategoryService = new CategoryService();
            _CategoryService.InsertCategory(model);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            _CategoryService = new CategoryService();
            var obj = _CategoryService.Find(id);
            return View(obj);
            
        }


        [HttpPost]
        public ActionResult Edit(Core.Domain.Category Model)
        {
            _CategoryService = new CategoryService();
            _CategoryService.Update(Model);
            return RedirectToAction("Index");
        }
    }
}