using NUnit.Framework;
using OpenQA.Selenium;
using WebPortalTest.E2E.Selectors;
using WebPortalTest.E2E.Tests;

namespace WebPortalTest.E2E.Helper
{
    public class LoginCoffeShopHelper
    {
        public static void Login(IWebDriver webDriver, string login, string password)
        {
            webDriver.Url = $"{AuthTests.BASE_URL}";

            webDriver
                .FindElement(Layout.LoginLink)
                .Click();

            webDriver
                .FindElement(LoginPage.UserNameInput)
                .SendKeys(login);

            webDriver
                .FindElement(LoginPage.PasswordInput)
                .SendKeys(password);

            webDriver
                .FindElement(LoginPage.LoginButton)
                .Click();

            var greetingsText = webDriver
                .FindElement(By.CssSelector(".greetings"))
                .Text;

            Assert.That(greetingsText.Contains(login));
        }

        public static void LoginAsModeratorCoffee(IWebDriver webDriver)
            => Login(webDriver, "AXA", "Azazin4455");
    }
}
