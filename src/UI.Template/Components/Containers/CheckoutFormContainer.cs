using OpenQA.Selenium;
using UI.Template.Components.Basic;
using UI.Template.Framework.Extensions;

namespace UI.Template.Components.Containers;

public class CheckoutFormContainer(By locator) : BaseComponent(locator)
{
    public TextInput FirstName => new(By.XPath($"{Locator.ToSelector()}//*[@id='firstName']"));
    public TextInput LastName => new(By.XPath($"{Locator.ToSelector()}//*[@id='lastName']"));
    public TextInput Email => new(By.XPath($"{Locator.ToSelector()}//*[@id='email']"));
    public TextInput Phone => new(By.XPath($"{Locator.ToSelector()}//*[@id='phoneNumber']"));
    public TextInput Street => new(By.XPath($"{Locator.ToSelector()}//*[@id='street']"));
    public TextInput City => new(By.XPath($"{Locator.ToSelector()}//*[@id='city']"));
    public TextInput ZipCode => new(By.XPath($"{Locator.ToSelector()}//*[@id='zipCode']"));
    public DropDownList PaymentMethod => new(By.XPath($"{Locator.ToSelector()}//*[@id='paymentMethod']"));
    public Button PayButton => new(By.XPath($"{Locator.ToSelector()}//button[@class='pay-button']"));
    public static Simple OrderTotal => new(By.XPath("//span[@ko-id='order-total-value']"));
    public static Simple SelectedPaymentMethod => new(By.XPath("//p[@ko-id='order-paymentMethod']"));


    /// <summary>
    /// Sets the name of the product.
    /// </summary>
    /// <param name="value">The name to set.</param>
    /// <returns>True if the name was set correctly, false otherwise.</returns>
    public bool SetFirstName(string value)
    {
        FirstName.Clear();
        FirstName.SendKeys(value);
        return FirstName.GetValue().Equals(value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Sets the last name of the customer.
    /// </summary>
    /// <param name="value">The last name to set.</param>
    /// <returns>True if the last name was set correctly, false otherwise.</returns>
    public bool SetLastName(string value)
    {
        LastName.Clear();
        LastName.SendKeys(value);
        return LastName.GetValue().Equals(value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Sets the email of the product.
    /// </summary>  
    /// <param name="value">The email to set.</param>
    /// <returns>True if the email was set correctly, false otherwise.</returns>
    public bool SetEmail(string value)
    {
        Email.Clear();
        Email.SendKeys(value);
        return Email.GetValue().Equals(value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Sets the phone of the product.
    /// </summary>
    /// <param name="value">The phone to set.</param>
    /// <returns>True if the phone was set correctly, false otherwise.</returns>
    public bool SetPhone(string value)
    {
        //Phone.Clear();
        Phone.SendKeys(value);
        return Phone.GetValue().Equals(value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Sets the street of the product.
    /// </summary>
    /// <param name="value">The street to set.</param>
    /// <returns>True if the street was set correctly, false otherwise.</returns>
    public bool SetStreet(string value)
    {
        Street.Clear();
        Street.SendKeys(value);
        return Street.GetValue().Equals(value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Sets the city of the product.
    /// </summary>
    /// <param name="value">The city to set.</param>
    /// <returns>True if the city was set correctly, false otherwise.</returns>
    public bool SetCity(string value)
    {
        City.Clear();
        City.SendKeys(value);
        return City.GetValue().Equals(value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Sets the zip code of the product.
    /// </summary>
    /// <param name="value">The zip code to set.</param>
    /// <returns>True if the zip code was set correctly, false otherwise.</returns>
    public bool SetZipCode(string value)
    {
        ZipCode.Clear();
        ZipCode.SendKeys(value);
        return ZipCode.GetValue().Equals(value, StringComparison.Ordinal);
    }

    /// <summary>
    /// Sets the payment method.
    /// </summary>
    /// <param name="value">The payment method to set.</param>
    /// <returns>True if the payment method was set correctly, false otherwise.</returns>
    public bool SetPaymentMethod(string paymentMethod)
    {
        PaymentMethod.SelectByText(paymentMethod);
        return PaymentMethod.GetSelectedOptionText() == paymentMethod;
    }

    /// <summary>
    /// Clicks the pay button to complete the checkout process.
    /// </summary>
    /// <returns>True if the pay button was clicked successfully, false otherwise.</returns>
    /// <remarks>Ensure that all required fields are filled out correctly before calling this method.</remarks>
    public bool ClickPayButton()
    {
        if (PayButton.IsEnabled())
        {
            PayButton.Click();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets the total amount of the order.
    /// </summary>
    /// <returns>The total amount of the order as a decimal.</returns>
    /// <remarks>Ensure that the order total element is visible and contains a valid decimal value before calling this method.</remarks>
    public static string GetOrderTotal()
    {
        return OrderTotal.GetText();
    }

    /// <summary>
    /// Gets the selected payment method for the order.
    /// </summary>
    /// <returns>The selected payment method as a string.</returns>
    public static string GetSelectedPaymentMethod()
    {
        return SelectedPaymentMethod.GetText();
    }
}
