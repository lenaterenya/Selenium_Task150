using OpenQA.Selenium;
using NUnit.Framework;

namespace Task_150_Final.Pages
{
    public class HomePage
    {
        private static IWebDriver _driver;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement emailInput => _driver.FindElement(By.Id("email_create"));
        public IWebElement createAnAccountButton => _driver.FindElement(By.Id("SubmitCreate"));
        public IWebElement emailEdit => _driver.FindElement(By.Id("email"));
        public IWebElement passwordEdit => _driver.FindElement(By.Id("passwd"));
        public IWebElement signInButton => _driver.FindElement(By.Id("SubmitLogin"));
        public IWebElement cartButton => _driver.FindElement(By.XPath("//a[@title='View my shopping cart']"));
        public IWebElement logOutButton => _driver.FindElement(By.ClassName("logout"));
        public IWebElement accountTitle => _driver.FindElement(By.XPath("//span[contains(text(), 'My account')]"));


        public void CreateNewAccount(string email)
        {
            emailInput.SendKeys(email);
            createAnAccountButton.Click();
        }

        public void LogIn(string email, string password)
        {
            emailEdit.SendKeys(email);
            passwordEdit.SendKeys(password);
            signInButton.Click();

            Assert.That(accountTitle.Displayed);

        }

        public void LogOut()
        {
            logOutButton.Click();
        }
    }
}

