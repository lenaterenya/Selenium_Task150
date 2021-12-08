using OpenQA.Selenium;

namespace Task_150_Final
{
    public class ScreenShot
    {
        public static byte[] Take(IWebDriver driver)
        {
            return ((ITakesScreenshot) driver).GetScreenshot().AsByteArray;
        }
    }
}