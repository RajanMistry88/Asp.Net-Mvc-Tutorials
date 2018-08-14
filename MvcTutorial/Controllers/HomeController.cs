using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTutorial.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            if (Session["UserID"] != null)
            {
                ViewBag.userName = Session["UserName"];
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Mvc Tutorial Application";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }

        public ActionResult Designing()
        {
            return View();
        }

        public ActionResult Information()
        {
            return View();
        }

        public ActionResult Logout()
        {

            Session.RemoveAll();
            return RedirectToAction("Login", "User");

        }
    }
}