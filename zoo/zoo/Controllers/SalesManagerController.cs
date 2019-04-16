using zoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace zoo.Controllers
{
    public class SalesManagerController : Controller
    {
        // GET: SalesManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return RedirectToAction("Index", "Login");
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
                String contactInfo = " Phone: " + Phone + " " + ", " + " Email: " + Email;
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
                if (ItemName != null)
                    ViewBag.Message = ItemInfo;
                else
                    ViewBag.Message = "No Item Found";
                return View();
            }
        }

        public ActionResult Shop()
        {
            team4zooEntities DB = new team4zooEntities();

            List<Shop> shoplist = DB.Shops.ToList();
            return View(shoplist);
        }

        [HttpPost]
        public ActionResult ViewInventory(Inventory Model)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                List<Inventory> inventorylist = db.Inventories.ToList();
                return View(inventorylist);
            }

                
           
        }
    }
}