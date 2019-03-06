using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftgunZonen.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string TokenKey { get; set; }
        public int ProductID { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
    }
}