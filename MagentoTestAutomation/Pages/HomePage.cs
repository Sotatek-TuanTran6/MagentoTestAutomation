using OpenQA.Selenium;

public class HomePage
{
    private readonly IWebDriver _driver;

    public HomePage(IWebDriver driver)
    {
        _driver = driver;
    }

    private IWebElement JacketsMenu => _driver.FindElement(By.LinkText("Jackets"));
    private IWebElement PantsMenu => _driver.FindElement(By.LinkText("Pants"));

    public void GoToJackets()
    {
        JacketsMenu.Click();
    }

    public void GoToPants()
    {
        PantsMenu.Click();
    }
}
