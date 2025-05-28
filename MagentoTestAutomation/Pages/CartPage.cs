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
        // Đợi loader ẩn đi
        _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("checkout-loader")));

        // Dùng selector rõ ràng hơn
        var proceedBtnSelector = By.CssSelector("button[data-role='proceed-to-checkout']");

        // Đợi nút hiện và có thể click
        var proceedBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(proceedBtnSelector));

        // Scroll tới nút
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", proceedBtn);

        // Optional: delay nhẹ để trang ổn định
        System.Threading.Thread.Sleep(500);

        try
        {
            proceedBtn.Click();
        }
        catch (ElementClickInterceptedException)
        {
            // Click bằng JS nếu click thường không được
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", proceedBtn);
        }
    }

}
