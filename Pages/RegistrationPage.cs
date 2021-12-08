using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace Task_150_Final.Pages
{
    public class RegistrationPage
    {
        private static IWebDriver _driver;
        public RegistrationPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement firstNameEdit => _driver.FindElement(By.Id("customer_firstname"));
        public IWebElement lastNameEdit => _driver.FindElement(By.Id("customer_lastname"));
        public IWebElement emailEdit => _driver.FindElement(By.Id("email"));
        public IWebElement password => _driver.FindElement(By.Id("passwd"));
        public IWebElement addressEdit => _driver.FindElement(By.Id("address1"));
        public IWebElement stateElement => _driver.FindElement(By.Id("id_state"));
        public IWebElement zipEdit => _driver.FindElement(By.Id("postcode"));
        public IWebElement phoneEdit => _driver.FindElement(By.Id("phone_mobile"));
        public IWebElement cityEdit => _driver.FindElement(By.Id("city"));
        public IWebElement registerButton => _driver.FindElement(By.Id("submitAccount"));
        public IWebElement accountTitle => _driver.FindElement(By.XPath("//span[contains(text(), 'My account')]"));

        public void RegisterAccount(UserInfo user)
        {

            firstNameEdit.SendKeys(user.FirstName);
            lastNameEdit.SendKeys(user.LastName);

            if (emailEdit.Text != user.Email)
            {
                emailEdit.Clear();
                emailEdit.SendKeys(user.Email);
            }

            password.SendKeys(user.Password);
            addressEdit.SendKeys(user.Address);

            SelectElement selectState = new SelectElement(stateElement);
            selectState.SelectByValue("2");

            zipEdit.SendKeys(user.PostCode);
            phoneEdit.SendKeys(user.Phone);
            cityEdit.SendKeys(user.City);
            registerButton.Click();

            Assert.That(accountTitle.Displayed);
        }
    }
}

