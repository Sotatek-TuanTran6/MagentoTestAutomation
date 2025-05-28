using OpenQA.Selenium;

public class LoginPage
{
    private readonly IWebDriver _driver;

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
    }

    // Locator
    private IWebElement EmailInput => _driver.FindElement(By.Id("email"));
    private IWebElement PasswordInput => _driver.FindElement(By.Id("pass"));
    private IWebElement SignInButton => _driver.FindElement(By.Id("send2"));

    // Action
    public void Login(string email, string password)
    {
        EmailInput.Clear();
        EmailInput.SendKeys(email);
        PasswordInput.Clear();
        PasswordInput.SendKeys(password);
        SignInButton.Click();
    }
}
