using System;
using OpenQA.Selenium;

namespace Task_150_Final
{
    public class HelperMethods
    {
        private static IWebDriver _driver;

        public HelperMethods(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool IsElementExist(String xpath)
        {
            try
            {
                _driver.FindElement(By.XPath(xpath));
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
            return true;
        }
    }
}
