using UI.Template.Pages;
using UI.Template.Components.Containers;
using OpenQA.Selenium;

namespace UI.Template.Tests;

[TestFixture]
public class TC4 : BaseTest
{
    [Test]
    public void AddProductToAdminTest()
    {
        //** STEP 1 **/
        HomePage homePage = new HomePage();
        homePage.Open();

        //** STEP 2 + 3 **/
        
        bool addedToBasket = homePage.AddToBasketFirstProductFromCategory("Monitors");
        Assert.That(addedToBasket, Is.True, "Product was not added to the basket.");
        ProductDetailPage productDetail = homePage.OpenProductByNameFromCategory("Monitors", "4K LED Monitor");
        
        //** STEP 4 **/
        productDetail.Header.OpenBasketContainer();

        //** STEP 5 **/
        Assert.That(productDetail.ProductInfoForm.GetName(), Is.EqualTo("4K LED Monitor"), "Product name does not match");
        Assert.That(productDetail.ProductInfoForm.GetPrice(), Is.EqualTo(300), "Product price does not match");
        Assert.That(productDetail.Header.GetBasketCount(), Is.EqualTo(1), "Basket count is not 1. Check Add to Cart functionality.");

        //** STEP 6 **/
        productDetail.Header.ClickCheckoutButton();

        //** STEP 7 **/
        //Validně vyplnit všechna povinná pole: First Name, Last Name, Street, City, ZIP Code, Email, Phone Number
        CheckoutFormContainer editCheckoutInfoContainer = new(By.XPath("//div[@class='checkout-form']"));
        Assert.Multiple(() =>
        {
            Assert.That(editCheckoutInfoContainer.SetFirstName("John"), Is.True, "Failed to set first name");
            Assert.That(editCheckoutInfoContainer.SetLastName("Doe"), Is.True, "Failed to set last name");
            Assert.That(editCheckoutInfoContainer.SetStreet("Hlavní 1"), Is.True, "Failed to set street");
            Assert.That(editCheckoutInfoContainer.SetCity("Praha"), Is.True, "Failed to set city");
            Assert.That(editCheckoutInfoContainer.SetZipCode("12000"), Is.True, "Failed to set ZIP code");
            Assert.That(editCheckoutInfoContainer.SetEmail("john.doe@example.com"), Is.True, "Failed to set e-mail");
            Assert.That(editCheckoutInfoContainer.SetPhone("123456789"), Is.True, "Failed to set phone number");
        });

        //** STEP 8 **/
        //payment method = PayPal
        Assert.That(editCheckoutInfoContainer.SetPaymentMethod("PayPal"), Is.True, "Failed to set payment method");

        //** STEP 9 **/
        editCheckoutInfoContainer.PayButton.Click();

        //** STEP 10 **/
        Assert.That(CheckoutFormContainer.GetSelectedPaymentMethod(), Is.EqualTo("Payment Method: PayPal"), "Payment method does not match expected value.");
        Assert.That(CheckoutFormContainer.GetOrderTotal(), Is.EqualTo("$ 300.00"), "Order total does not match expected value.");
    }
}
