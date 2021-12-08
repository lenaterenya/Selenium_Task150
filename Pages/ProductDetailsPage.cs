using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Task_150_Final.Pages
{
    public class ProductDetailsPage
    {
        private static IWebDriver _driver;
        public ProductDetailsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement addToWishListButton => _driver.FindElement(By.Id("wishlist_button"));
        public IWebElement successIcon => _driver.FindElement(By.XPath("//i[@class='icon-ok']"));
        public IWebElement productName => _driver.FindElement(By.XPath("//h1[contains(@itemprop,'name')]"));
        public IWebElement productFrame => _driver.FindElement(By.XPath("//iframe[contains(@id, 'fancybox')]"));
        public IWebElement addToCartButton => _driver.FindElement(By.Id("add_to_cart"));

        public string GetProductName()
        {
            return productName.Text;
        }
        public bool IsProductAddedToCart(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var helper = new HelperMethods(_driver);
            return helper.IsElementExist("successIcon");
        }

        public bool IsProductDetailDisplayed(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var helper = new HelperMethods(_driver);
            return helper.IsElementExist("productFrame");
        }
    }
}
