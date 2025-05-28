using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace MagentoTestAutomation.Drivers
{
    public static class DriverFactory
    {
        public static IWebDriver Driver { get; private set; }

        public static void Init()
        {
            Driver = new FirefoxDriver();
            Driver.Manage().Window.Maximize();
        }

        public static void Cleanup()
        {
            Driver.Quit();
        }
    }
}
