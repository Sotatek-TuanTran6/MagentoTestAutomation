using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

public class MagentoPurchaseTests : IAsyncLifetime
{
    private IWebDriver _driver;
    private LoginPage _loginPage;
    private ProductPage _productPage;
    private CartPage _cartPage;
    private CheckoutPage _checkoutPage;

    public async Task InitializeAsync()
    {
        // Setup driver (Firefox)
        _driver = new FirefoxDriver();

        // Khởi tạo các page object
        _loginPage = new LoginPage(_driver);
        _productPage = new ProductPage(_driver);
        _cartPage = new CartPage(_driver);
        _checkoutPage = new CheckoutPage(_driver);

        // Mở website
        _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
    }

    public async Task DisposeAsync()
    {
        // Đóng browser sau khi test xong
        _driver.Quit();
    }

    [Fact]
    public void Test_UserCanLoginAndPurchaseItems()
    {
        // 1. Chuyển sang trang login trước khi nhập
        _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/customer/account/login/");

        // 2. Login
        _loginPage.Login("tuantran8@gmail.com", "c3guzxmLsGsD@@G");

        // 3. Mua 2 áo Jacket nam
        _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/men/tops-men/jackets-men.html");
        _productPage.AddProductsToCart(2);

        // 4. Mua 1 quần Pants nam
        _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/men/bottoms-men/pants-men.html");
        _productPage.AddProductsToCart(1);

        // 5. Vào giỏ hàng và thanh toán
        _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/checkout/cart/");
        _cartPage.ProceedToCheckout();

        // 6. Nhập địa chỉ giao hàng
        _checkoutPage.EnterDeliveryAddress("Tran", "Tuan", "123 Main St", "New York", "New York", "10001", "United States", "1234567890");

        // 7. Chọn phương thức giao hàng
        _checkoutPage.SelectDeliveryMethod();

        // 8. Đặt hàng
        _checkoutPage.PlaceOrder();


        Assert.True(true);
    }

}
