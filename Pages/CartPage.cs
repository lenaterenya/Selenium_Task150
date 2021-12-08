using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Task_150_Final.Pages
{
    public class CartPage
    {
        private static IWebDriver _driver;
        private string _productName = "//td/p[@class='product-name']";

        public CartPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement productDescription => _driver.FindElement(By.XPath(_productName));
        public IWebElement productQuantity => _driver.FindElement(By.XPath("//input[@class= 'cart_quantity_input form-control grey']"));

        public List<string> GetProductNamesInCart()
        {
            return _driver.FindElements(By.XPath(_productName)).Select(x => x.Text).ToList();
        }
    }
}
