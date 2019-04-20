﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
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

                ViewBag.Message = "New Department Created";

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

        //This creates a SQL ready department object and adds to database
        public void SaveDepartment(Department department)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=den1.mssql8.gear.host;Database=team4zoo;Uid=team4zoo;Pwd=Ji627i1J-x5?"; //Connection String with login info.

                //Call Query
                SqlCommand cmd = new SqlCommand("SaveDepartment", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //Set Parameters
                SqlParameter paramDName = new SqlParameter();//DepartmentName
                paramDName.ParameterName = "@department_name";
                paramDName.Value = department.department_name;
                cmd.Parameters.Add(paramDName);

                SqlParameter paramID = new SqlParameter();//ZooName
                paramID.ParameterName = "@Department_ID";
                paramID.Value = department.Department_ID;
                cmd.Parameters.Add(paramID);

                SqlParameter paramDRev = new SqlParameter();//Revenue
                paramDRev.ParameterName = "@dep_revenue";
                paramDRev.Value = department.dep_revenue;
                cmd.Parameters.Add(paramDRev);

                SqlParameter paramDEXP = new SqlParameter(); //Expenditures
                paramDEXP.ParameterName = "@dep_expenditure";
                paramDEXP.Value = department.dep_expenditure;
                cmd.Parameters.Add(paramDEXP);


                conn.Open(); //Opens connection
                cmd.ExecuteNonQuery(); //Add to table
            }
        }

        [HttpPost]
        public ActionResult editDept(Department department)
        {

            if (ModelState.IsValid && department.department_name.Length > 0 && department.dep_expenditure >= 0)
            {    
                AdminController adminController = new AdminController();

                adminController.SaveDepartment(department);

                ViewBag.Message = "Edit Successful";

                return RedirectToAction("Dept");
            }
            else
            {
                ViewBag.Message = "Invalid Input";
                return View();
            }
        }

        //This creates a SQL ready Employee object and adds to database
        public void AddEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=den1.mssql8.gear.host;Database=team4zoo;Uid=team4zoo;Pwd=Ji627i1J-x5?"; //Connection String with login info.

                //Call Query
                SqlCommand cmd = new SqlCommand("AddEmployee", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //Set Parameters
                SqlParameter paramFName = new SqlParameter();//FirstName
                paramFName.ParameterName = "@f_name";
                paramFName.Value = employee.f_name;
                cmd.Parameters.Add(paramFName);


                SqlParameter parammidName = new SqlParameter(); //Middle Init
                parammidName.ParameterName = "@mid_init";
                if(employee.mid_init != null)
                {
                    parammidName.Value = employee.mid_init;
                }
                else
                {
                    parammidName.Value = DBNull.Value;
                }
                cmd.Parameters.Add(parammidName);

                SqlParameter paramLName = new SqlParameter();//Last Name
                paramLName.ParameterName = "@l_name";
                paramLName.Value = employee.l_name;
                cmd.Parameters.Add(paramLName);

                SqlParameter paramEmail = new SqlParameter();//Email
                paramEmail.ParameterName = "@email";
                paramEmail.Value = employee.email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramSex = new SqlParameter();//Sex
                paramSex.ParameterName = "@sex";
                paramSex.Value = employee.sex;
                cmd.Parameters.Add(paramSex);

                SqlParameter paramDOB = new SqlParameter();//DOB
                paramDOB.ParameterName = "@dob";
                paramDOB.Value = employee.dob;
                cmd.Parameters.Add(paramDOB);

                SqlParameter paramSSN = new SqlParameter();//SSN
                paramSSN.ParameterName = "@ssn";
                paramSSN.Value = employee.ssn;
                cmd.Parameters.Add(paramSSN);

                SqlParameter paramHourW = new SqlParameter(); //Hourly Wage
                paramHourW.ParameterName = "@hourly_wage";
                paramHourW.Value = employee.hourly_wage;
                cmd.Parameters.Add(paramHourW);

                SqlParameter paramHireD = new SqlParameter();//Hire DAte
                paramHireD.ParameterName = "@hire_date";
                paramHireD.Value = employee.hire_date;
                cmd.Parameters.Add(paramHireD);

                SqlParameter paramPhone = new SqlParameter();//Phone
                paramPhone.ParameterName = "@phone_num";
                paramPhone.Value = employee.phone_num;
                cmd.Parameters.Add(paramPhone);

                SqlParameter paramRole = new SqlParameter();//RoleID
                paramRole.ParameterName = "@Role_ID";
                if(employee.Role_ID != null)
                {
                    paramRole.Value = employee.Role_ID;
                }
                else
                {
                    paramRole.Value = DBNull.Value;
                }

                cmd.Parameters.Add(paramRole);

                SqlParameter paramDept = new SqlParameter(); //Department
                paramDept.ParameterName = "@Department_ID";
                if (employee.Department_ID != null)
                {
                    paramDept.Value = employee.Department_ID;
                }
                else
                {
                    paramDept.Value = DBNull.Value;
                }
                cmd.Parameters.Add(paramDept);

                SqlParameter paramActive = new SqlParameter();//isActive
                paramActive.ParameterName = "@isActive";
                paramActive.Value = employee.isActive;
                cmd.Parameters.Add(paramActive);

                SqlParameter paramDisplay = new SqlParameter();//Display NAme
                paramDisplay.ParameterName = "@display_name";
                paramDisplay.Value = employee.display_name;
                cmd.Parameters.Add(paramDisplay);

                SqlParameter paramSup = new SqlParameter();//Supervisor
                paramSup.ParameterName = "@Supervisor_ID";
                if(employee.Supervisor_ID != null)
                {
                    paramSup.Value = employee.Supervisor_ID;
                }
                else
                {
                    paramSup.Value = DBNull.Value;
                }
                cmd.Parameters.Add(paramSup);

                conn.Open(); //Opens connection
                cmd.ExecuteNonQuery(); //Add to table
            }
        }


        public ActionResult Staff()
        {


                return View();
        }


        //Two Lists for Add Employee
        public ContentResult GetDepartmentList()
        {
            using (team4zooEntities DB = new team4zooEntities())
            {
                List<DepartmentListModel> deptList = new List<DepartmentListModel>();

                //Get Department List
                var results = DB.Departments.ToList();

                //Convert all necessary data to List.
                foreach (Department department in results)
                {
                    DepartmentListModel deptVM = new DepartmentListModel();
                    //Assign Values

                    //Check if underscore
                    if (department.department_name.Contains('_'))
                    {
                        string tmpname = department.department_name.Replace("_", " ");
                        deptVM.department_name = tmpname;
                    }
                    else
                    {
                        deptVM.department_name = department.department_name;
                    }

                    deptVM.Department_ID = department.Department_ID;


                    //Add to Chart Data
                    deptList.Add(deptVM);
                }

                //Return serialised Json
                return Content(JsonConvert.SerializeObject(deptList), "application/json");

            }
        }

        public ContentResult GetRoleList()
        {
            using (team4zooEntities DB = new team4zooEntities())
            {
                List<RoleListModel> rolelist = new List<RoleListModel>();

                //Get Role List
                var results = DB.Roles.ToList();

                //Convert all necessary data to List.
                foreach (Role role in results)
                {
                    RoleListModel roleVM = new RoleListModel();
                    //Assign Values

                    //Check if underscore
                    if (role.Job_Title.Contains('_'))
                    {
                        string tmpname = role.Job_Title.Replace("_", " ");
                        roleVM.Job_Title = tmpname;
                    }
                    else
                    {
                        roleVM.Job_Title = role.Job_Title;
                    }

                    //Assign Role ID
                    roleVM.Role_ID = role.Role_ID;


                    //Add to Chart Data
                    rolelist.Add(roleVM);
                }

                //Return serialised Json
                return Content(JsonConvert.SerializeObject(rolelist), "application/json");

            }
        }


        [HttpGet]
        public ActionResult addEmp()
        {


            return View();
        }

        [HttpPost]
        public ActionResult addEmp(FormCollection formCollection)
        {
            DateTime dateC;
            if (formCollection["f_name"].Length <= 0)
            {
                ViewBag.Message = "Unable to Add, First Name cannot be blank!";
                return View();
            }
            else if (formCollection["l_name"].Length <= 0)
            {
                ViewBag.Message = "Unable to Add, Last Name cannot be blank!";
                return View();
            }
            else if (formCollection["email"].Length <= 0)
            {
                ViewBag.Message = "Unable to Add, Email cannot be blank!";
                return View();
            }
            else if (formCollection["ssn"].Length < 9)
            {
                ViewBag.Message = "Unable to Add, SSN must be 9 digits!";
                return View();
            }
            else if(formCollection["phone_num"].Length < 12)
            {
                ViewBag.Message = "Unable to Add, phone number format incorrect!";
                return View();
            }
            else if(Convert.ToDouble(formCollection["hourly_wage"]) < 7.25)
            {
                ViewBag.Message = "Unable to Add, hourly wage cannot be lower than the minimum wage ($7.25)!";
                return View();
            }

            //Check Input
            if (ModelState.IsValid)
            {
                //Assign Values
                Employee employee = new Employee();
                employee.f_name = formCollection["f_name"];
                if (formCollection["mid_init"].Length > 0) //If Middle initial exists, add
                {
                    employee.mid_init = formCollection["mid_init"];
                }
                else
                {
                    employee.mid_init = null;
                }

                employee.l_name = formCollection["l_name"];
                employee.email = formCollection["email"];
                employee.sex = formCollection["sex"];
                employee.dob = DateTime.Parse(formCollection["dob"]);
                employee.ssn = formCollection["ssn"];
                employee.hire_date = DateTime.Parse(formCollection["hire_date"]);
                employee.phone_num = formCollection["phone_num"];
                employee.hourly_wage = Convert.ToDecimal(formCollection["hourly_wage"]);


                //Checked Box condition check
                if (formCollection["isActive"] == "true,false")
                {
                    employee.isActive = true;
                }
                else
                {
                    employee.isActive = false;
                }

                if (formCollection["Department_ID"] != "null")
                {
                    Guid DeptID = new Guid(formCollection["Department_ID"]);
                    employee.Department_ID = DeptID;
                }
                else
                {
                    employee.Department_ID = null;
                }

                if (formCollection["Role_ID"] != "null")
                {
                    Guid roleID = new Guid(formCollection["Role_ID"]);
                    employee.Role_ID = roleID;
                }
                else
                {
                    employee.Role_ID = null;
                }

                //Get Department's Supervisor ID
                if (formCollection["Department_ID"] != "null")
                {
                    using (team4zooEntities DB = new team4zooEntities())
                    {
                        Guid id = new Guid(formCollection["Department_ID"]);
                        Department department = DB.Departments.Single(Department => Department.Department_ID == id);
                        employee.Supervisor_ID = department.Supervisor_ID;
                    }
                }
                else
                {
                    employee.Supervisor_ID = null;
                }
                //Set DisplayName
                employee.display_name = employee.f_name + " " + employee.l_name[0];

                //INSERT ADD FUNCTION HERE
                AdminController adminController = new AdminController();
                adminController.AddEmployee(employee);
                ViewBag.Message = "New Employee Added!";

                return RedirectToAction("Staff");

            }
            else
            {
                ViewBag.Message = "Unable to Add, Error Input";
                return View();
            }
        }


        public ActionResult Maintenance()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Finance()
        {

            return View();
        }
       
        public ContentResult RevExpChart()
        {
            using (team4zooEntities DB = new team4zooEntities())
            {
                //Chart View Data Model to store necessary data.
                List<DeptChartViewModel> dept = new List<DeptChartViewModel>();

                //Get Department List
                var results = DB.Departments.ToList();

                //Convert all necessary data to List.
                foreach(Department department in results)
                {
                    DeptChartViewModel deptVM = new DeptChartViewModel();
                    //Assign Values
                    
                    //Check if underscore
                    if (department.department_name.Contains('_'))
                    {
                        string tmpname = department.department_name.Replace("_", " ");
                        deptVM.name = tmpname;
                    }
                    else
                    {
                        deptVM.name = department.department_name;
                    }

                    deptVM.expenditure = department.dep_expenditure;
                    deptVM.revenue = department.dep_revenue;

                    //Add to Chart Data
                    dept.Add(deptVM);
                }

                //Return serialised Json
                return Content(JsonConvert.SerializeObject(dept), "application/json");
            }
        }
        
        [HttpGet]
        public ActionResult Report()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Report(String item)
        {
            return View();
        }


    }
}