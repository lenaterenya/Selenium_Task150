using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Task_150__Final;
using Task_150_Final.Pages;
using Allure.Commons;
using Allure.NUnit.Attributes;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;


namespace Task_150_Final
{
    [TestFixture]
    [Category("Final Tests"), Category("NUnit"), Category("IShop")]
    public class AutoTests : AllureReport
    {
        private IWebDriver _driver;

        private Uri _testUrl =
            new Uri("http://automationpractice.com/index.php?controller=authentication&back=my-account");

        private string _email = "7sdhg3@mail.ru";
        private string _email2 = "8g447687s@mail.ru";
        private string _email3 = "2dffdgshg7s@mail.ru";
        private string _password = "qwert!@#";
        public static string sauceUserName = "Lenaa";
        public static string sauceAccessKey = "2dd675c5-2746-4909-afff-34f21f8606aa";
        public String URL = "https://ondemand.eu-central-1.saucelabs.com:443/wd/hub";

        [SetUp]
        public void Initialize()
        {
            string env = "Selenoid";

            switch (env)
            {
                case "Selenoid":
                    var firefoxOption = new FirefoxOptions();
                    //firefoxOption.PlatformName = "Windows 10";
                    firefoxOption.BrowserVersion = "91.0";
                    
                    _driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), firefoxOption);
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    _driver.Manage().Window.Maximize();
                    break;

                case "SauceLab":
                    var browserOptions = new ChromeOptions();
                    browserOptions.PlatformName = "Windows 8.1";
                    browserOptions.BrowserVersion = "75.0";
                    var sauceOptions = new Dictionary<string, object>();
                    browserOptions.AddAdditionalOption("sauce:options", sauceOptions);
                    sauceOptions.Add("username", sauceUserName);
                    sauceOptions.Add("accessKey", sauceAccessKey);
                   
                    _driver = new RemoteWebDriver(new Uri(URL), browserOptions.ToCapabilities(), TimeSpan.FromSeconds(600));
                    break;

                default:
                    _driver = new ChromeDriver((@"C:\chromedriver"));
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    _driver.Manage().Window.Maximize();
                    break;
            }
        }

        [AllureSubSuite("Functional Tests")]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Critical)]
        [AllureTest("New user registration")]
        [Test]
        public void RegisterTheAccount()
        {
            var user = new UserInfo(_email, _password);
            _driver.Navigate().GoToUrl(_testUrl);

            var homepage = new HomePage(_driver);
            homepage.CreateNewAccount(_email);

            var registration = new RegistrationPage(_driver);
            registration.RegisterAccount(user);
        }

        [AllureSubSuite("Functional Tests")]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Critical)]
        [AllureTest("Login the account")]
        [Test]
        public void LoginTheAccount()
        {
            var homepage = new HomePage(_driver);
            _driver.Navigate().GoToUrl(_testUrl);

            homepage.LogIn(_email, _password);
        }

        [AllureSubSuite("Functional Tests")]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Critical)]
        [AllureTest("Add Product To Auto Created WishList")]
        [Test]
        public void AddProductToAutoCreatedWishList()
        {
            var homepage = new HomePage(_driver);
            _driver.Navigate().GoToUrl(_testUrl);

            homepage.LogIn(_email, _password);

            var myAccount = new MyAccountPage(_driver);
            myAccount.wishlistButton.Click();

            var wishList = new WishListPage(_driver);
            Assert.False(wishList.WishListTableIsDisplayed(_driver), "Existing WishList is present");

            var productsPage = new ProductsPage(_driver);
            productsPage.AddProductToWishList(Enums.Categories.TShirts);

            wishList.NavigateToWishList();
            wishList.WishListTableIsDisplayed(_driver);
        }

        [AllureSubSuite("Functional Tests")]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Critical)]
        [AllureTest("Add Product To Existed WishList")]
        [Test]
        public void AddProductToExistedWishList()
        {
            var user = new UserInfo(_email2, _password);
            _driver.Navigate().GoToUrl(_testUrl);

            var homepage = new HomePage(_driver);
            homepage.CreateNewAccount(_email2);

            var registration = new RegistrationPage(_driver);
            registration.RegisterAccount(user);

            var myAccount = new MyAccountPage(_driver);
            myAccount.wishlistButton.Click();

            var wishList = new WishListPage(_driver);
            wishList.CreateNewWishList("My WishList");

            var productsPage = new ProductsPage(_driver);
            var dressName = productsPage.AddProductToWishList(Enums.Categories.Women);

            wishList.NavigateToWishList();
            wishList.WishListTableIsDisplayed(_driver);

            Assert.IsTrue(wishList.VerifyWishListContainsProduct(dressName), $"WishList doesn't contain {dressName}");
        }

        [AllureSubSuite("Functional Tests")]
        [AllureSeverity(Allure.Commons.Model.SeverityLevel.Critical)]
        [AllureTest("Add Product To Cart")]
        [Test]
        public void AddingTheProductToCart()
        {
            var user = new UserInfo(_email3, _password);
            _driver.Navigate().GoToUrl(_testUrl);

            var homepage = new HomePage(_driver);
            homepage.CreateNewAccount(_email3);

            var registration = new RegistrationPage(_driver);
            registration.RegisterAccount(user);

            var productsPage = new ProductsPage(_driver);

            var products = new List<string>();
            products.Add(productsPage.AddProductToCart(Enums.Categories.Women));
            products.Add(productsPage.AddProductToCart(Enums.Categories.Dresses));
            products.Add(productsPage.AddProductToCart(Enums.Categories.TShirts));

            var cart = new CartPage(_driver);
            homepage.cartButton.Click();

            var actualProductsInCart = cart.GetProductNamesInCart();

            Assert.AreEqual(products, actualProductsInCart,
                "Added Products are not present in Cart or added incorrect products");
        }

        [TearDown]
        public void FinishTest()
        {
            string env = "Default";

            switch (env)
            {
                case "SauceLab":
                    var passed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
                    if (_driver != null)
                    {
                        //all driver operations should happen here after the check
                        ((IJavaScriptExecutor) _driver).ExecuteScript(
                            "sauce:job-result=" + (passed ? "passed" : "failed"));
                        _driver.Quit();
                    }

                    break;

                default:
                    _driver.Quit();
                    break;
            }
        }
    }
}
