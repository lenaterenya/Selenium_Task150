using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Task_150__Final;

namespace Task_150_Final.Pages
{
    public class ProductsPage
    {
        private static IWebDriver _driver;

        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement productsTable => _driver.FindElement(By.XPath("//ul[@id= 'homefeatured']"));
        public IWebElement wishlistButton => _driver.FindElement(By.Id("wishlist_button"));

        private Uri _dressCategory = new Uri("http://automationpractice.com/index.php?id_category=8&controller=category");
        private Uri _tShirtsCategory = new Uri("http://automationpractice.com/index.php?id_category=5&controller=category");
        private Uri _womenCategory = new Uri("http://automationpractice.com/index.php?id_category=3&controller=category");
        public IWebElement closeButton => _driver.FindElement(By.XPath("//span[@class='cross']"));

        public Uri GetCategoryUrl(Enum category)
        {
            switch (category)
            {
                case Enums.Categories.Dresses:
                    return new Uri("http://automationpractice.com/index.php?id_category=8&controller=category");
                    break;
                case Enums.Categories.TShirts:
                    return new Uri("http://automationpractice.com/index.php?id_category=5&controller=category");
                    break;
                case Enums.Categories.Women:
                    return new Uri("http://automationpractice.com/index.php?id_category=3&controller=category");
                    break;
                default: throw new ArgumentException("Invalid category");
            }
        }
        public void NavigateToProductCategory(Enum category)
        {
            _driver.Navigate().GoToUrl(GetCategoryUrl(category));
        }

        public IEnumerable<IWebElement> GetProductElements()
        {
            return _driver.FindElements(By.XPath("//ul[contains(@class,'product_list grid row')]/li//a[contains(@class,'product_img_link')]"));
        }

        public string AddProductToWishList(Enum productCategory)
        {
            var productDetails = new ProductDetailsPage(_driver);
            NavigateToProductCategory(productCategory);
            
            var productElements = GetProductElements();
            productElements.First().Click();
            
            productDetails.IsProductDetailDisplayed(_driver);
            _driver.SwitchTo().Frame(productDetails.productFrame);

            var productName = productDetails.GetProductName();
            productDetails.addToWishListButton.Click();
            productDetails.IsProductAddedToCart(_driver);

            return productName;
        }

        public string AddProductToCart(Enum productCategory)
        {
            var productDetails = new ProductDetailsPage(_driver);
            var random = new Random();

            NavigateToProductCategory(productCategory);
            
            var productElements = GetProductElements();
            productElements.OrderBy(x => random.Next(1, productElements.Count())).FirstOrDefault().Click();

            productDetails.IsProductDetailDisplayed(_driver);
            _driver.SwitchTo().Frame(productDetails.productFrame);

            var productName = productDetails.GetProductName();
            productDetails.addToCartButton.Click();

            _driver.SwitchTo().ParentFrame();
            productDetails.IsProductAddedToCart(_driver);
            closeButton.Click();

            return productName;
        }
    }
}

