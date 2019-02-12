using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace AlphaPoint_QA.Common
{
    public class UserSetFunctions
    {
        public static void EnterText(IWebElement element, string value)
        {
            element.Clear();
            element.SendKeys(value);
        }

        //Click into a button, checkbox, option etc
        public static void Click(IWebElement element)
        {
            element.Click();
        }

        public static void Clear(IWebElement element)
        {
            element.Clear();
        }

        //Selecting a dropdown control
        public static void SelectDropdown(IWebElement element, string value)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(value);
        }


        public static void VerifyWebElement(IWebElement we)
        {
            try
            {
                Assert.True(we.Displayed && we.Enabled);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
