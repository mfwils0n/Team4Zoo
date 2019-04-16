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

        // GET: CareGiverHome
        public ActionResult Index()
        {
            //            IList<Animal> AnimalList = MyAnimals();
            //            List<MyAnimal> Animals = new List<MyAnimal>();
            //            foreach (var item in AnimalList)
            //           {
            //                Animals.Add(new MyAnimal(item.animal_name, GetFamilyName(item.family), GetExihibitN(item.Exhibit_ID), GetExihibitL(item.Exhibit_ID)));
            //            }
            //            return View(Animals);
            return View();
        }

        public IList<Employee> GetEmployees(Employee Manager)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                System.Guid MyEmployee_ID = (System.Guid)Session["Employee_ID"];
                var result = new List<Employee>();

                var employees = db.Employees.Where(e => e.Supervisor_ID == MyEmployee_ID).ToList();

                foreach (var employee in employees)
                {
                    result.Add(employee);
                    result.AddRange(GetEmployees(employee));
                }
                return result;
            }
        }
    }
}