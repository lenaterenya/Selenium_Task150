using OpenQA.Selenium;

namespace Task_150_Final.Pages
{
    public class MyAccountPage
    {
        private static IWebDriver _driver;
        public MyAccountPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement signOutButton => _driver.FindElement(By.XPath("//a[@class='logout']"));
        public IWebElement userInfo => _driver.FindElement(By.XPath("//a[@class = 'account']"));
        public IWebElement wishlistButton => _driver.FindElement(By.XPath("//a[@title= 'My wishlists']"));


    }
}
