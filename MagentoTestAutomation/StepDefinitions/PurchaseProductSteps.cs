using TechTalk.SpecFlow;
using OpenQA.Selenium;
using MagentoTestAutomation.Drivers;
using Xunit;

[Binding]
public class PurchaseProductSteps
{
    private readonly IWebDriver _driver = DriverFactory.Driver;

    [Given(@"the user navigates to the Magento store")]
    public void GivenTheUserNavigatesToTheMagentoStore()
    {
        _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
    }

    [Given(@"logs in with valid credentials")]
    public void GivenLogsInWithValidCredentials()
    {
        _driver.FindElement(By.CssSelector(".panel.header .authorization-link a")).Click();
        _driver.FindElement(By.Id("email")).SendKeys("user@example.com");
        _driver.FindElement(By.Id("pass")).SendKeys("password123");
        _driver.FindElement(By.Id("send2")).Click();
    }

    [When(@"the user adds 2 Men's Jackets and 1 Men's Pants to the cart")]
    public void WhenUserAddsProductsToCart()
    {
    }

    [When(@"proceeds to checkout")]
    public void WhenUserProceedsToCheckout()
    {
        _driver.FindElement(By.CssSelector(".showcart")).Click();
        _driver.FindElement(By.CssSelector("#top-cart-btn-checkout")).Click();
    }

    [When(@"enters a valid delivery address and selects a delivery method")]
    public void WhenUserEntersAddressAndDelivery()
    {
        _driver.FindElement(By.CssSelector("input[name='ko_unique_1']")).Click();
    }

    [When(@"places the order")]
    public void WhenUserPlacesTheOrder()
    {
        _driver.FindElement(By.CssSelector(".checkout")).Click();
    }

    [Then(@"the order should be listed under ""(.*)""")]
    public void ThenTheOrderShouldBeListed(string section)
    {
        _driver.FindElement(By.CssSelector(".account")).Click();
        _driver.FindElement(By.LinkText("My Orders")).Click();
        var order = _driver.FindElements(By.CssSelector(".order-id"));
        Assert.True(order.Count > 0);
    }
}
