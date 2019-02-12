using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaPoint_QA.Utils
{
    public static class Users
    {
        static  Users()
        {

        }
        public static UserCredential User1 { get; set; }
        public static UserCredential User2 { get; set; }
        public static UserCredential User3 { get; set; }
        public static UserCredential User4 { get; set; }

    }

    public class UserCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
