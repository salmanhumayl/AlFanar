using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AlFahimCMS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username ,string password, string returnUrl)
        {

            if (username == "admin" && password=="admin")
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(username,false);
                Session["UserName"]= "admin";
                return RedirectToAction("Dashboard", "Dashboard");

            }
           
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}