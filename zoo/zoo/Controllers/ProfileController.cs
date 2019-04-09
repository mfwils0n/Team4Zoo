using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zoo.Models;

namespace zoo.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        //checks if username already exists, if it does, you may not change your username
        public bool CheckExistingUser(UserProfile userModel)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                var User = db.Credentials.Where(x => x.username == userModel.newUsername).FirstOrDefault();
                //check if user exists in the database already
                if (User == null) //not in db
                {
                    return false;
                }
                else
                    return true; //return true if user is in db
            }

        }

        [HttpPost]
        public ActionResult EditUsername(UserProfile userModel)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                //Set Username and password from the view
                string oldUsername = Session["username"].ToString();
                string newUsername = userModel.newUsername;
                string password = userModel.oldPassword;
                var userDetails = db.Credentials.Where(x => x.username == oldUsername && x.password == password).FirstOrDefault();

                if (userDetails == null || CheckExistingUser(userModel) == true) //wrong username/password or username already exists
                {
                    return RedirectToAction("Index", "Profile");
                }

                else
                {
                    db.Database.ExecuteSqlCommand("update zoo.Credentials set username = '" + newUsername + "' where username = '" + oldUsername + "'");
                    //log out user after the change
                    System.Guid Employee_ID = (System.Guid)Session["Employee_ID"];
                    Session.Abandon();
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        public ActionResult EditPassword(UserProfile userModel)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                //Set Username Password
                string oldUsername = Session["username"].ToString();
                string newPassword = userModel.newPassword;
                string oldPassword = userModel.oldPassword;
                var userDetails = db.Credentials.Where(x => x.username == oldUsername && x.password == oldPassword).FirstOrDefault();

                if (userDetails == null) //wrong username/password 
                {
                    return RedirectToAction("Index", "Profile");
                }

                else
                {
                    db.Database.ExecuteSqlCommand("update zoo.Credentials set password = '" + newPassword + "' where username = '" + oldUsername + "'");
                    //logout
                    System.Guid Employee_ID = (System.Guid)Session["Employee_ID"];
                    Session.Abandon();
                    return RedirectToAction("Index", "Home");
                }

            }
        }

        [HttpPost]
        public ActionResult EditEmail(UserProfile userModel)
        {
            using (team4zooEntities db = new team4zooEntities())
            {                
                System.Guid Employee_ID = (System.Guid)Session["Employee_ID"];
                string newEmail = userModel.newEmail;
                db.Database.ExecuteSqlCommand("update zoo.Employee set email = '" + newEmail + "' where Employee_ID = '" + Employee_ID + "'");             
                Session.Abandon();
                return RedirectToAction("Index", "Home");
               
            }
        }

        [HttpPost]
        public ActionResult EditPhone(UserProfile userModel)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                System.Guid Employee_ID = (System.Guid)Session["Employee_ID"];
                string newPhone = userModel.newPhone;
                db.Database.ExecuteSqlCommand("update zoo.Employee set phone_num = '" + newPhone + "' where Employee_ID = '" + Employee_ID + "'");
                Session.Abandon();
                return RedirectToAction("Index", "Home");

            }
        }
    }
}