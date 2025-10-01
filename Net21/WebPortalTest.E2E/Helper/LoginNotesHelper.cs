using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebPortalTest.E2E.Common;
using WebPortalTest.E2E.Selectors.Notes;

namespace WebPortalTest.E2E.Helper;

public class LoginNotesHelper
{
    public static void Login(IWebDriver webDriver, string login, string password)
    {
        webDriver.Url = $"{Constants.NOTES_BASE_URL}";

        webDriver
            .FindElement(LayoutNotes.LoginLink)
            .Click();

        webDriver
            .FindElement(LoginPageNotes.UserNameInput)
            .SendKeys(login);

        webDriver
            .FindElement(LoginPageNotes.PasswordInput)
            .SendKeys(password);

        webDriver
            .FindElement(LoginPageNotes.LoginButton)
            .Click();

        var wait = new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(1000));
        var greetingsText = wait
            .Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".nickname")))
            .Text;

        Assert.That(greetingsText.Contains(login));
    }

    public static void LoginAsAdmin(IWebDriver webDriver)
        => Login(webDriver, TestNotesConfiguration.TestAdminLogin, TestNotesConfiguration.TestAdminPassword);
}