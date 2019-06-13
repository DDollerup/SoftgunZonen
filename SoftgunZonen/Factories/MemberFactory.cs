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
            string SQL = $"SELECT * FROM Member WHERE Email='{email}'";

            Member member = SqlQuery(SQL);

            if (member?.Password == AutoFactory.GenerateSaltedPassword(member.Token, password))
            {
                return member;
            }
            return new Member();
        }
    }
}