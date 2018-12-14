using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftgunZonen.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public decimal Price { get; set; }
        public bool Avaliable { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }

        public string GetAvaliable()
        {
            return Avaliable ? "green" : "red";
        }
    }
}