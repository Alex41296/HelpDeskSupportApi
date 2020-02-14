using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPILab.Models
{
    public class IssueModel
    {
        public IssueModel() 
        { 
        }

        public int Id { get; set; }
        public Nullable<int> Id_Client { get; set; }
        public string Description { get; set; }
        public Nullable<DateTime> Time_Stamp { get; set; }
        public string Contact_Phone { get; set; }
        public string Contact_Email { get; set; }
        public string Classification { get; set; }
        public string Status { get; set; }
        public string Service_Type { get; set; }
        public string Name { get; set; }
        public string First_Name { get; set; }
        public string Second_Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Second_contact { get; set; }
        public string Email { get; set; }


    }

}