using UI.Template.Pages;
using UI.Template.Components.Containers;
using OpenQA.Selenium;

namespace UI.Template.Tests;

[TestFixture]
public class TC4bug : BaseTest
{
    [Test]
    public void CheckoutFlowTest()
    {
        //** STEP 1 **/
        HomePage homePage = new HomePage();
        homePage.Open();

        //** STEP 2 **/
        
        //bool addedToBasket = homePage.AddToBasketFirstProductFromCategory("Cameras");
        //Assert.That(addedToBasket, Is.True, "Product was not added to the basket.");
        /*
        použil bych tuto metodu, ale z nějakého důvodu AddToCartButton v kategorii Cameras nefunguje 
        (u všech ostatních kategorií funguje bez problémů)
        proto používám 'okliku' a otevírám detail produktu a přidávám do košíku tam
        */

        ProductDetailPage productDetail = homePage.OpenProductByNameFromCategory("Cameras", "DSLR Camera X200");

        //** STEP 3 **/
        productDetail.ProductInfoForm.AddToCart();
        
        //** STEP 4 **/
        productDetail.Header.OpenBasketContainer();

        //** STEP 5 **/
        Assert.That(productDetail.ProductInfoForm.GetName(), Is.EqualTo("DSLR Camera X200"), "Product name does not match");
        Assert.That(productDetail.ProductInfoForm.GetPrice(), Is.EqualTo(700), "Product price does not match");
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
        /*  V testu nelze pokračovat, protože se zobrazuje chybová hláška:
            "The following products cannot be bought: DSLR Camera X200"
            Alternativa je vypracobána v TC4.cs, kde je místo produktu "DSLR Camera X200" použit "4K LED Monitor",
            který lze bez problémů přidat do košíku a dokončit objednávku.
        */
    }
}
