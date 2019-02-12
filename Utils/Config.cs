using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlphaPoint_QA.Utils
{
    public class Config
    {
        public TestEnvironment UserPortal { get; set; }

        public TestEnvironment AdminPortal { get; set; }

        public string Platform { get; set; } 

        public string Browser { get; set; }

    }

}
