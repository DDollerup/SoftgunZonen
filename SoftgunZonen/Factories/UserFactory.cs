using SoftgunZonen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftgunZonen.Factories
{
    public class UserFactory : AutoFactory<User>
    {
        public User Login(string email, string password)
        {
            string SQL = $"SELECT * FROM [User] WHERE Email='{email}'";

            User member = SqlQuery(SQL);

            if (member?.Password == AutoFactory.GenerateSaltedPassword(member.Token, password))
            {
                return member;
            }
            return new User();
        }
    }
}