using SoftgunZonen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftgunZonen.Factories
{
    public class MemberFactory : AutoFactory<Member>
    {
        public Member Login(string email, string password)
        {
            string hashedPassword = GenerateSHA512Hash(password);

            string SQL = $"SELECT * FROM Member WHERE Email='{email}' AND Password='{hashedPassword}'";

            return SqlQuery(SQL);
        }
    }
}