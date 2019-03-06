using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftgunZonen.Models
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public List<CommentVM> Comments { get; set; }
    }
}