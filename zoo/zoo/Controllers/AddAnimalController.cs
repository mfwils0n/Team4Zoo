using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zoo.Models;

namespace zoo.Controllers
{
    public class AddAnimalController : Controller
    {
        // GET: AddAnimal
        public ActionResult Index()
        {
            AddAnimal Obj = new AddAnimal();
            Obj.familyNames = GetFamilyNames();
            Obj.emplyeeUserNames = ViewMyEmployees();
            Obj.exhibitNames = GetExihibitNames();
            Obj.attrNames = GetAttrNames();
            List<char> sex = new List<char>();
            sex.Add('M'); sex.Add('F');
            Obj.sexs = sex;
            return View(Obj);
        }

        public IEnumerable<string> ViewMyEmployees()
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                System.Guid MyEmployee_ID = (System.Guid)Session["Employee_ID"];
                return db.Employees.ToList().Where(x => (x.Supervisor_ID == MyEmployee_ID)).Select(y => y.username);
            }
        }

        public IEnumerable<string> GetFamilyNames()
        {
            using (team4zooEntities db = new team4zooEntities())
            {
               return db.Family_Name.ToList().Select(y => y.family_title);
            }
        }

        public IEnumerable<string> GetExihibitNames()
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                return db.Exhibits.ToList().Select(y => y.exhibit_name);
            }
        }

        public IEnumerable<string> GetAttrNames()
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                return db.Attractions.ToList().Select(y => y.attraction_name);
            }
        }

        [HttpPost]
        public ActionResult AddAnimal(AddAnimal model, DateTime dob)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                Animal animal = new Animal();
                animal.Animal_ID = Guid.NewGuid();
                animal.animal_name = model.name;
                animal.dob = dob;
                animal.sex = model.sex;
                animal.weight = model.weight;
                animal.owner = model.owner;
                animal.isActive = true;
                animal.family = db.Family_Name.Where(x => x.family_title == model.familyN).Select(y => y.Family_ID).FirstOrDefault();
                if (model.AttrN != null) {
                    animal.Attraction_ID = db.Attractions.Where(x => x.attraction_name == model.AttrN).Select(y => y.Attraction_ID).FirstOrDefault();
                 }
                animal.Exhibit_ID = db.Exhibits.Where(x => x.exhibit_name == model.ExhibitN).Select(y => y.Exhibit_ID).FirstOrDefault();
                animal.Assignee1_ID = db.Employees.Where(x => x.username == model.Assignee1).Select(y => y.Employee_ID).FirstOrDefault();
                animal.Assignee2_ID = db.Employees.Where(x => x.username == model.Assignee2).Select(y => y.Employee_ID).FirstOrDefault();
                animal.Assignee3_ID = db.Employees.Where(x => x.username == model.Assginee3).Select(y => y.Employee_ID).FirstOrDefault();

                db.Animals.Add(animal);
                db.SaveChanges();
                return RedirectToAction("Index", "AddAnimal");
            }

        }
    }
}