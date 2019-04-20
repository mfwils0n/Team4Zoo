using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zoo.Models
{
    public class AdminModel
    {
    }


    public class DeptChartViewModel
    {
        public string name { get; set; }
        public decimal revenue { get; set; }
        public decimal expenditure { get; set; }
    }

    public class DepartmentListModel
    {
        public string department_name { get; set; }
        public Guid Department_ID { get; set; }
    }

    public class RoleListModel
    {
        public string Job_Title { get; set; }
        public Guid Role_ID { get; set; }
    }
}