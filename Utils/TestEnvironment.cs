using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaPoint_QA.Utils
{
    public class TestEnvironment
    {
        public string PortalUrl { get; set; }

        public string PortalServerUrl { get; set; }

        public Dictionary<string, UserCredential> Users { get; set; }

    }


    public class UserCredential
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
