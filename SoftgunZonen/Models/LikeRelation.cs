using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftgunZonen.Models
{
    public class LikeRelation
    {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public int ProductID { get; set; }
    }
}