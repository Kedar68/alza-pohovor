using UI.Template.Pages;
using UI.Template.Components.Containers;

namespace UI.Template.Tests;

[TestFixture]
public class TC3 : BaseTest
{
    [Test]
    public void AddProductToAdminTest()
    {
        //** STEP 1 **/
        AdminPage adminPage = new AdminPage();
        adminPage.Open();

        //** STEP 2 **/
        EditProductContainer editProductContainer = adminPage.OpenAddNewProductContainer();
        
        Assert.Multiple(() =>
        {
            Assert.That(editProductContainer.SetName("Camera M25"), Is.True, "Failed to set product name");
            Assert.That(editProductContainer.SelectCategory("Cameras"), Is.True, "Failed to select category");
            Assert.That(editProductContainer.SetPrice(50.5m), Is.True, "Failed to set product price");
            Assert.That(editProductContainer.SetStock(5), Is.True, "Failed to set product stock");
            Assert.That(editProductContainer.SelectImage("Camera 2"), Is.True, "Failed to select image");
            Assert.That(editProductContainer.SetDescription("Camera"), Is.True, "Failed to set product description");
        });


        //** STEP 3 **/
        editProductContainer.SaveChanges();

        //** STEP 4 **/
        HomePage homePage = adminPage.GoToEshopHome();


        //** STEP 5 + 6 **/
        ProductDetailPage productDetail = homePage.OpenProductByNameFromCategory("Cameras", "Camera M25");

        //** STEP 7 **/
        Assert.Multiple(() =>
        {
            Assert.That(productDetail.ProductInfoForm.GetName(), Is.EqualTo("Camera M25"), "Product name does not match");
            Assert.That(productDetail.ProductInfoForm.GetPrice(), Is.EqualTo(50.5m), "Product price does not match");
            Assert.That(productDetail.ProductInfoForm.GetStockStatus(), Is.EqualTo(5), "Product stock does not match");
        });
    }
}
