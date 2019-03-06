using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftgunZonen.Models
{
    public class Category
    {
        // int står for Integer og kan kun indeholde heltal
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}