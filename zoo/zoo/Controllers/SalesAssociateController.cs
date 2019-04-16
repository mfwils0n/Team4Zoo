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

        public ActionResult TicketSales()
        {

            return View();
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
                if (ItemName != null)
                    ViewBag.Message = ItemInfo;
                else
                    ViewBag.Message = "No Item Found";
                return View();
            }
        }
        [HttpPost]
        public ActionResult Sales(Shop_Sale_Record Model)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                var Price = db.Inventories.Where(x => x.Item_ID == Model.Item_ID).Select(y => y.price).FirstOrDefault();
                var InStock = db.Inventories.Where(x => x.Item_ID == Model.Item_ID).Select(y => y.ordered_quantity).FirstOrDefault();
                var ItemName = db.Inventories.Where(x => x.Item_ID == Model.Item_ID).Select(y => y.item_name).FirstOrDefault();
                var amountPurchased = Model.quantity;
                var cost = Price * Model.quantity;
                if (InStock >= amountPurchased)
                {
                    Shop_Sale_Record NewEntry = new Shop_Sale_Record();
                    NewEntry.Sale_ID = System.Guid.NewGuid();
                    NewEntry.Shop_ID = Model.Shop_ID;
                    NewEntry.Item_ID = Model.Item_ID;
                    NewEntry.quantity = Model.quantity;
                    NewEntry.Customer_ID = Model.Customer_ID;
                    NewEntry.refund_flag = Model.refund_flag;
                    NewEntry.date = DateTime.Today;
                    db.Shop_Sale_Record.Add(NewEntry);
                    db.SaveChanges();
                    if(Model.refund_flag == false)
                    ViewBag.Message = "Transaction Complete: " + Model.quantity + " " + ItemName + " costs $" + cost;
                    else
                        ViewBag.Message = "Succsefully Refunded: " + Model.quantity + " " + ItemName + " for $" + cost;
                }
                else
                    ViewBag.Message = "Transaction Failed: We only have " + InStock + " " + ItemName + " in stock."; 
                return View();
            }
        }
        [HttpPost]
        public ActionResult TicketSales(Shop_Sale_Record Model, BoxOffice_Records Model2)
        {
            using (team4zooEntities db = new team4zooEntities())
            {
                var InStock = db.Inventories.Where(x => x.Item_ID == Model.Item_ID).Select(y => y.ordered_quantity).FirstOrDefault();
                var Price = db.Inventories.Where(x => x.Item_ID == Model.Item_ID).Select(y => y.price).FirstOrDefault();
                var cost = Model.quantity * Price;
                var amountLeft = InStock - Model.quantity;
                decimal discount = 3;
                var totalCost = cost - discount;
                Guid dept = new Guid("7203f98e-e4dc-4edd-b221-98b5855fd4e3");
                if (Model.refund_flag == true)
                {
                    BoxOffice_Records NewEntry = new BoxOffice_Records();
                    NewEntry.Sale_ID = System.Guid.NewGuid();
                    NewEntry.Department_ID = dept;
                    NewEntry.membership_discount = discount;
                    NewEntry.quantity = Model.quantity;
                    NewEntry.Customer_ID = Model.Customer_ID;
                    NewEntry.tot_amt_paid_after_discounts = totalCost;
                    NewEntry.date = DateTime.Today;
                    db.BoxOffice_Records.Add(NewEntry);
                    db.SaveChanges();
                    ViewBag.Message = "Transaction Completed";
                    db.Database.ExecuteSqlCommand("update zoo.Inventory set ordered_quantity = '" + amountLeft + "' where Item_Id = '" + Model.Item_ID + "'");

                }
                else
                {
                    BoxOffice_Records NewEntry = new BoxOffice_Records();
                    NewEntry.Sale_ID = System.Guid.NewGuid();
                    NewEntry.Department_ID = dept;
                    NewEntry.membership_discount = discount;
                    NewEntry.quantity = Model.quantity;
                    NewEntry.Customer_ID = Model.Customer_ID;
                    NewEntry.tot_amt_paid_after_discounts = totalCost;
                    NewEntry.date = DateTime.Today;
                    db.BoxOffice_Records.Add(NewEntry);
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("update zoo.Inventory set ordered_quantity = '" + amountLeft + "' where Item_Id = '" + Model.Item_ID + "'");
                    ViewBag.Message = "Transaction Completed";
                }
               
                return View();
            }
        }
    }
}
