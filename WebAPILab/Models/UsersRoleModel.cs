﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPILab.Models
{
    public class UsersRoleModel
    {
        public int Id { get; set; }
        public Nullable<int> Id_Users { get; set; }
        public Nullable<int> Id_Role { get; set; }
    }
}