using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class ProductPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public ProductPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void GoTo(string url)
    {
        _driver.Navigate().GoToUrl(url);
    }

    private IReadOnlyCollection<IWebElement> Products =>
        _driver.FindElements(By.CssSelector(".product-item"));

    public void AddProductsToCart(int numberOfProducts)
    {
        int count = 0;
        foreach (var product in Products)
        {
            if (count >= numberOfProducts)
                break;

            try
            {
                var productLink = product.FindElement(By.CssSelector("a.product-item-link"));
                productLink.Click();

                _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".swatch-attribute.size")));
                _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".swatch-attribute.color")));

                var sizeOptions = _driver.FindElements(By.CssSelector(".swatch-attribute.size .swatch-option"));
                if (sizeOptions.Count > 0)
                    sizeOptions[0].Click();

                var colorOptions = _driver.FindElements(By.CssSelector(".swatch-attribute.color .swatch-option"));
                if (colorOptions.Count > 0)
                    colorOptions[0].Click();

                var addToCartBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.action.tocart")));
                addToCartBtn.Click();

                _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".message-success")));

                count++;

                _driver.Navigate().Back();
                _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".product-item")));

            }
            catch (Exception)
            {
                _driver.Navigate().Back();
                continue;
            }
        }
    }

}
