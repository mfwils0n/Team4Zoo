using zoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace zoo.Controllers
{
    public class SalesAssociateController : Controller
    {
        // GET: SalesAssociate
        public ActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Sales()
        {

            return View();
        }

        public ActionResult Inventory()
        {

            return View();
        }
        public ActionResult Contact()
        {

            return View();
        }
            [HttpPost]
           public ActionResult Contact(Employee Model)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                var Phone = db.Employees.Where(x => x.f_name == Model.f_name && x.l_name == Model.l_name).Select(y => y.phone_num).FirstOrDefault();
                var Email = db.Employees.Where(x => x.f_name == Model.f_name && x.l_name == Model.l_name).Select(y => y.email).FirstOrDefault();
                String contactInfo = " Phone: " + Phone + " " + ", " +  " Email: " + Email;
                if (Phone != null && Email != null)
                    ViewBag.Message = contactInfo;
                else
                    ViewBag.Message = "No one by that name was found";
                return View();
            }
       }

        [HttpPost]
        public ActionResult Inventory(Inventory Model)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                var Price = db.Inventories.Where(x => x.item_name == Model.item_name || x.Item_ID == Model.Item_ID).Select(y => y.price).FirstOrDefault();
                var InStock = db.Inventories.Where(x => x.item_name == Model.item_name || x.Item_ID == Model.Item_ID).Select(y => y.ordered_quantity).FirstOrDefault();
                var ItemName = db.Inventories.Where(x => x.item_name == Model.item_name || x.Item_ID == Model.Item_ID).Select(y => y.item_name).FirstOrDefault();

                String ItemInfo = ItemName + " Price: " + Price + " " + ", " + " In Stock: " + InStock;
                ViewBag.Message = ItemInfo;
                return View();
            }
        }
    }
}
