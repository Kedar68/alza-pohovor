using OpenQA.Selenium;
using UI.Template.Framework.Extensions;

namespace UI.Template.Components.Containers;

public class ProductGridContainer(By locator) : BaseComponent(locator)
{
    public Dictionary<string, CheckoutCard> GetProductCards()
    {
        Dictionary<string, CheckoutCard> productCards = new Dictionary<string, CheckoutCard>();
        By productCardXPathLocator = By.XPath(Locator.ToSelector() + "//div[@class='product-card']");

        if (!Wait.TryWaitWithCondition(() => WebDriver.FindElements(productCardXPathLocator).Count > 0, timeout: 5))
        {
            Logger.LogWarning("There are no products in the product grid container.");
            return productCards;
        }

        int productCardsCount = WebDriver.FindElements(productCardXPathLocator).Count;
        for (int i = 1; i <= productCardsCount; i++)
        {
            CheckoutCard productCard = new(By.XPath($"({productCardXPathLocator.ToSelector()})[{i}]"));
            productCard.ScrollTo();
            productCards.Add(productCard.GetName(), productCard);
        }

        return productCards;
    }
}
