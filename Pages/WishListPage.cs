using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Telerik.JustMock;

namespace Task_150_Final.Pages
{
    public class WishListPage
    {
        private static IWebDriver _driver;
        public WishListPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement wishlistTable => _driver.FindElement(By.XPath("//table[@class='table table-bordered']"));
        public IWebElement wishlistNameField => _driver.FindElement(By.Id("name"));
        public IWebElement wishlistSaveButton => _driver.FindElement(By.Id("submitWishlist"));
        public IWebElement wishlistViewButton => _driver.FindElement(By.XPath("//a[contains(@onclick, 'WishlistManage')]"));
        public IWebElement wishlistProductNameLable => _driver.FindElement((By.Id("s_title")));
        public string wishlistUrl = "http://automationpractice.com/index.php?fc=module&module=blockwishlist&controller=mywishlist";

        public void NavigateToWishList()
        {
            _driver.Navigate().GoToUrl(wishlistUrl);
        }

        public void CreateNewWishList(string wishListName)
        {
            wishlistNameField.SendKeys(wishListName);
            wishlistSaveButton.Click();
        }

        public bool VerifyWishListContainsProduct(string productName)
        {
            wishlistViewButton.Click();
            Wait.For(3);
            Assert.That(wishlistProductNameLable.Displayed); 
            return wishlistProductNameLable.Text.Contains(productName);
        }

        public bool WishListTableIsDisplayed(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var helper = new HelperMethods(_driver);
            return helper.IsElementExist("wishlistTable");
        }
    }
}
