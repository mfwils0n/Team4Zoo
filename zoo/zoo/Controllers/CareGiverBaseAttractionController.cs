using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zoo.Models;

namespace zoo.Controllers
{
    public class CareGiverBaseAttractionController : Controller
    {
        // GET: CareGiverBaseAttraction
        public ActionResult Index()
        {
            IEnumerable<Animal> MyAnimals = ViewMyAnimals();
            var tuple = new Tuple<IEnumerable<Animal>>(MyAnimals);
            return View(tuple);
        }

        public IEnumerable<Animal> ViewMyAnimals()
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                System.Guid Employee_ID = (System.Guid)Session["Employee_ID"];
                return db.Animals.ToList().Where(x => (x.Assignee1_ID == Employee_ID || x.Assignee2_ID == Employee_ID || x.Assignee3_ID == Employee_ID));
            }
        }

        [HttpPost]
        public ActionResult ViewMyAnimalCurrentAttr(Animal model)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                System.Guid AttrID = (System.Guid)db.Animals.Where(x => x.animal_name == model.animal_name).Select(y => y.Attraction_ID).FirstOrDefault();
                if (AttrID == null)
                {
                    return RedirectToAction("Index", "CareGiverBaseAttraction");
                }
                else
                {
                    IEnumerable<Attraction> AttrReport = getNames(db.Attractions.Where(x => x.Attraction_ID == AttrID && x.isActive == true).ToList());
                    var tuple = new Tuple<IEnumerable<Attraction>>(AttrReport);
                    return View("~/Views/CareGiverBaseAttraction/CurrentAttr.cshtml", tuple);
                }

            }
        }

        [HttpPost]
        public ActionResult AnimalAttrHistory(Animal model)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                System.Guid AttrID = (System.Guid)db.Animals.Where(x => x.animal_name == model.animal_name).Select(y => y.Attraction_ID).FirstOrDefault();
                if (AttrID == null)
                {
                    return RedirectToAction("Index", "CareGiverReport");
                }
                else
                {
                    IEnumerable<Attraction> AttrReport = getNames(db.Attractions.Where(x => x.Attraction_ID == AttrID).ToList());

                    var tuple = new Tuple<IEnumerable<Attraction>>(AttrReport);

                    return View("~/Views/CareGiverBaseAttraction/AttrHistory.cshtml", tuple);
                }

            }

        }

        public IEnumerable<Attraction> getNames(IEnumerable<Attraction> MyAttr){

            using (team4zooEntities db = new team4zooEntities())
            {
                foreach (var item in MyAttr)
                {
                    string Fname = db.Employees.Where(x => x.Employee_ID == item.manager_ID).Select(y => y.f_name).FirstOrDefault();
                    string LName = db.Employees.Where(x => x.Employee_ID == item.manager_ID).Select(y => y.l_name).FirstOrDefault();
                    item.manager_name = Fname + " " + LName;

                }
                return MyAttr;
            }
        }
    }
}