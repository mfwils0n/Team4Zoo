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
            IEnumerable<Animal> AnimalList = ViewMyAnimals();
            return View(AnimalList);
        }
        
        public IEnumerable<Animal> ViewMyAnimals()
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                System.Guid Employee_ID = (System.Guid)Session["Employee_ID"];
                return db.Animals.ToList().Where(x => (x.Assignee1_ID == Employee_ID || x.Assignee2_ID == Employee_ID || x.Assignee3_ID == Employee_ID));
            }
        }

    }
}