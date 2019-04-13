using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zoo.Models;

namespace zoo.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Dept()
        {
            team4zooEntities DB = new team4zooEntities();

            List<Department> deptlist = DB.Departments.ToList();
            return View(deptlist);
            
        }


        public ActionResult Staff()
        {
            return View();
        }
    }
}