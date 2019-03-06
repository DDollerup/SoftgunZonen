using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftgunZonen.Models
{
    public class MemberVM
    {
        public Member Member { get; set; }
        public List<Comment> Comments { get; set; }
    }
}