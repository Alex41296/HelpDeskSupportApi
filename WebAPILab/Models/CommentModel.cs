using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPILab.Models
{
    public class CommentModel
    {
        public CommentModel()
        {
        }

        public int Id { get; set; }
        public Nullable<int> Id_Issue { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<DateTime> Time_Stamp { get; set; }


    }

}