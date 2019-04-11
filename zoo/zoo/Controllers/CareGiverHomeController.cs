using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zoo.Models;

namespace zoo.Controllers
{
    public class CareGiverHomeController : Controller
    {
        // GET: CareGiverHome
        public ActionResult Index()
        {
            return View();
        }
        
        public ViewResult ViewMyAnimals(Animal Model)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                Animal MyAnimals = new Animal();
                System.Guid Employee_ID = (System.Guid)Session["Employee_ID"];
                MyAnimals.EmployeesAnimals = (IList)db.Database.SqlQuery<Animal>("select * from zoo.Animal" + " where Assignee1_ID = '" + Employee_ID + "' or Assignee2_ID = '" + Employee_ID + "' or Assignee3_ID = '" + Employee_ID).ToList();
                return View(MyAnimals);
            }
        }
    }
}