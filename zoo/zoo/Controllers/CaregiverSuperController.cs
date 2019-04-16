using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zoo.Models;

namespace zoo.Controllers
{
    public class CaregiverSuperController : Controller
    {

        // GET: CareGiverHome
        public ActionResult Index()
        {
            IEnumerable<Employee> EmployeeList = ViewMyEmployees();
            List<MyEmployee> Employees = new List<MyEmployee>();
            foreach (var person in EmployeeList)
            {
                Employees.Add(new MyEmployee(person.f_name, person.l_name, person.email, person.phone_num));
            }
            return View(Employees);
        }

        public IEnumerable<Employee> ViewMyEmployees()
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                System.Guid MyEmployee_ID = (System.Guid)Session["Employee_ID"];
                return db.Employees.ToList().Where(x => (x.Supervisor_ID == MyEmployee_ID));
            }
        }

        public String GetFamilyName(int? FamilyID)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                return db.Family_Name.Where(x => x.Family_ID == FamilyID).Select(y => y.family_title).FirstOrDefault();
            }

        }

        public String GetExihibitN(System.Guid ID)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                return db.Exhibits.Where(x => x.Exhibit_ID == ID).Select(y => y.exhibit_name).FirstOrDefault();
            }

        }

        public String GetExihibitL(System.Guid ID)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                return db.Exhibits.Where(x => x.Exhibit_ID == ID).Select(y => y.exhibit_loc).FirstOrDefault();
            }

        }
    }
}
