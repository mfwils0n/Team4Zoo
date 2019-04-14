using zoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace zoo.Controllers
{
    public class SalesManagerController : Controller
    {
        // GET: SalesManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Inventory()
        {
            ViewBag.Message = "Your current inventory page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Keep a tab on your employees.";

            return View();
        }
    }
}