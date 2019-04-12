using zoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace zoo.Controllers
{
    public class CaregiverSuperController : Controller
    {
        // GET: CaregiverSuper
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }
        public ActionResult MyAnimals()
        {
            ViewBag.Message = "Your animals page.";

            return View();
        }
        public ActionResult MyEmployees()
        {
            ViewBag.Message = "Your people page.";

            return View();
        }
        public ActionResult MyAttractions()
        {
            ViewBag.Message = "Your attractions page.";

            return View();
        }
    }
}