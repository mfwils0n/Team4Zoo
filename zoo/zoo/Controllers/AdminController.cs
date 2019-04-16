using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
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
            using (team4zooEntities DB = new team4zooEntities())
            {
               List<Department> deptlist = DB.Departments.ToList();

            return View(deptlist);
            }
        }



        [HttpGet] //HTTP GET to ensure addDept is reached
        public ActionResult AddDept()
        {

                return View();
        }

        //This creates a SQL ready department object and adds to database
        public void AddDepartment(Department department)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=den1.mssql8.gear.host;Database=team4zoo;Uid=team4zoo;Pwd=Ji627i1J-x5?"; //Connection String with login info.

                //Call Query
                SqlCommand cmd = new SqlCommand("AddDepartment", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //Set Parameters
                SqlParameter paramDName = new SqlParameter();//DepartmentName
                paramDName.ParameterName = "@department_name";
                paramDName.Value = department.department_name;
                cmd.Parameters.Add(paramDName);

                SqlParameter paramDZoo = new SqlParameter();//ZooName
                paramDZoo.ParameterName = "@zoo_name";
                paramDZoo.Value = department.zoo_name;
                cmd.Parameters.Add(paramDZoo);

                SqlParameter paramDRev = new SqlParameter();//Revenue
                paramDRev.ParameterName = "@dep_revenue";
                paramDRev.Value = department.dep_revenue;
                cmd.Parameters.Add(paramDRev);

                SqlParameter paramDSupID = new SqlParameter();//SupervisorID
                paramDSupID.ParameterName = "@Supervisor_ID";
                paramDSupID.Value = DBNull.Value;
                cmd.Parameters.Add(paramDSupID);

                SqlParameter paramDEXP = new SqlParameter(); //Expenditures
                paramDEXP.ParameterName = "@dep_expenditure";
                paramDEXP.Value = department.dep_expenditure;
                cmd.Parameters.Add(paramDEXP);


                conn.Open(); //Opens connection
                cmd.ExecuteNonQuery(); //Add to table
            }
        }

        [HttpPost]
        public ActionResult AddDept(FormCollection formCollection)
        {
            /*USE FOLLOWING CODE FOR TESTING KEYS
            if (ModelState.IsValid)
            {
                foreach (string key in formCollection.AllKeys)
                {
                    Response.Write("Key = " + key + "  ");
                    Response.Write("Value = " + formCollection[key]);
                    Response.Write("<br/>");
                }
            }
            */

            if (ModelState.IsValid && formCollection["department_name"].Length > 0 && formCollection["dep_revenue"].Length > 0 && formCollection["dep_expenditure"].Length > 0)
            {
                //Get Data From Form
                Department department = new Department();
                department.department_name = formCollection["department_name"];
                department.zoo_name = formCollection["zoo_name"];
                department.dep_revenue = Convert.ToDecimal(formCollection["dep_revenue"]);
                department.dep_expenditure = Convert.ToDecimal(formCollection["dep_expenditure"]);

                AdminController adminController = new AdminController();

                adminController.AddDepartment(department);

                return RedirectToAction("Dept");
            }
            else
            {
                ViewBag.Message = "Invalid Input";
                return View();
            }
        }


        [HttpGet]
        public ActionResult editDept(string id)
        {
            using (team4zooEntities DB = new team4zooEntities())
            {
                Guid ID = new Guid(id);
                Department department = DB.Departments.Single(Department => Department.Department_ID == ID);

                return View(department);
            }
        }

        public ActionResult Staff()
        {
            return View();
        }

        public ActionResult Maintenance()
        {
            return View();
        }
    }
}