using Microsoft.Extensions.Configuration;
using System;

namespace AlphaPoint_QA.Utils
{
    public static class TestData
    {
        private static IConfiguration testData ;

        static TestData()
        {
            testData = new ConfigurationBuilder().AddJsonFile(Const.TestDataFileName).Build();
        }

        public static string GetData(string key)
        {
            var data = testData[key];
            if (data == null) throw new Exception(String.Format("Data with key {0} not found", key));
            return data;
        }
    }
}
