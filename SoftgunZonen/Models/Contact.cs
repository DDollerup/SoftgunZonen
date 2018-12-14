using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftgunZonen.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        [AllowHtml]
        public string Content { get; set; }
    }
}