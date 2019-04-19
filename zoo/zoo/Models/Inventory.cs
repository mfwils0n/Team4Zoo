
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace zoo.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Inventory
{

    public Inventory()
    {

        this.Shop_Sale_Record = new HashSet<Shop_Sale_Record>();

    }


    public System.Guid Item_ID { get; set; }
        [DisplayName("Item Name")]
        public string item_name { get; set; }

    public System.DateTime last_ordered { get; set; }

    public Nullable<System.DateTime> resupply_date { get; set; }

    public decimal price { get; set; }
        [DisplayName("Quantity")]
        public int ordered_quantity { get; set; }

    public System.Guid Department_ID { get; set; }

    public decimal wholesaleprice { get; set; }



    public virtual ICollection<Shop_Sale_Record> Shop_Sale_Record { get; set; }

}

}
