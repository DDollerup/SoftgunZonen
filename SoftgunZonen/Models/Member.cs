﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftgunZonen.Models
{
    public class Member
    {
        public int ID { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int MemberRoleID { get; set; }
        public bool Changed { get; set; }
    }
}