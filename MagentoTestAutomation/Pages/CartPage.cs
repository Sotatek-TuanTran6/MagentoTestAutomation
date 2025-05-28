using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class CartPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public CartPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
    }

    public void ProceedToCheckout()
    {
        _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("checkout-loader")));

        var proceedBtnSelector = By.CssSelector("button[data-role='proceed-to-checkout']");

        var proceedBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(proceedBtnSelector));

        // Scroll tới nút
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", proceedBtn);

        System.Threading.Thread.Sleep(500);

        try
        {
            proceedBtn.Click();
        }
        catch (ElementClickInterceptedException)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", proceedBtn);
        }
    }

}
