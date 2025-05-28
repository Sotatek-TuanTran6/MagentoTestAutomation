using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

public class CheckoutPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public CheckoutPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
    }

    // Locators
    private IWebElement FirstNameInput => _driver.FindElement(By.Name("firstname"));
    private IWebElement LastNameInput => _driver.FindElement(By.Name("lastname"));
    private IWebElement StreetAddressInput => _driver.FindElement(By.Name("street[0]"));
    private IWebElement CityInput => _driver.FindElement(By.Name("city"));
    private IWebElement StateDropdown => _driver.FindElement(By.Name("region_id"));
    private IWebElement ZipInput => _driver.FindElement(By.Name("postcode"));
    private IWebElement CountryDropdown => _driver.FindElement(By.Name("country_id"));
    private IWebElement PhoneInput => _driver.FindElement(By.Name("telephone"));
    private IWebElement NextButton => _driver.FindElement(By.CssSelector("button[data-role='opc-continue']"));
    

    public void EnterDeliveryAddress(string firstName, string lastName, string street, string city, string state, string zip, string country, string phone)
    {
        WaitForLoaderToDisappear();

        _wait.Until(ExpectedConditions.ElementIsVisible(By.Name("firstname")));
        FirstNameInput.Clear();
        FirstNameInput.SendKeys(firstName);

        LastNameInput.Clear();
        LastNameInput.SendKeys(lastName);

        StreetAddressInput.Clear();
        StreetAddressInput.SendKeys(street);

        CityInput.Clear();
        CityInput.SendKeys(city);

        WaitForLoaderToDisappear();

        try
        {
            var selectState = new SelectElement(StateDropdown);
            selectState.SelectByText(state);
        }
        catch (NoSuchElementException)
        {
        }

        ZipInput.Clear();
        ZipInput.SendKeys(zip);

        var selectCountry = new SelectElement(CountryDropdown);
        selectCountry.SelectByText(country);

        PhoneInput.Clear();
        PhoneInput.SendKeys(phone);

        WaitForLoaderToDisappear();
        ClickElementSafe(NextButton);
    }

    public void SelectDeliveryMethod(string shippingMethodValue = null)
    {
        WaitForLoaderToDisappear();

        var shippingMethodRadios = _wait.Until(driver => driver.FindElements(By.CssSelector("input[type='radio'][name^='ko_unique_']")));
        if (shippingMethodRadios.Count == 0)
            throw new Exception("Không tìm thấy phương thức vận chuyển nào.");

        IWebElement radioToSelect = null;
        if (string.IsNullOrEmpty(shippingMethodValue))
        {
            radioToSelect = shippingMethodRadios[0];
        }
        else
        {
            foreach (var radio in shippingMethodRadios)
            {
                if (radio.GetAttribute("value") == shippingMethodValue)
                {
                    radioToSelect = radio;
                    break;
                }
            }

            if (radioToSelect == null)
                throw new Exception($"Không tìm thấy phương thức vận chuyển với value: {shippingMethodValue}");
        }

        ClickElementSafe(radioToSelect);

        WaitForLoaderToDisappear();

        var nextButton = _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button.continue")));
        ClickElementSafe(nextButton);
    }


    public void PlaceOrder()
    {
        WaitForLoaderToDisappear();

        _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.payment-method")));

        var placeOrderBtn = _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.CssSelector("button.action.primary.checkout")));

        ClickElementSafe(placeOrderBtn);
    }


    private void WaitForLoaderToDisappear()
    {
        _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("checkout-loader")));
    }

    private void ClickElementSafe(IWebElement element)
    {
        try
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            element.Click();
        }
        catch (ElementClickInterceptedException)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", element);
        }
    }
}
