using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebPortalTest.E2E.Selectors;

namespace WebPortalTest.E2E.Tests
{
    public class AuthTests
    {
        public const string BASE_URL = "https://localhost:7210";

        private IWebDriver _webDriver;

        [OneTimeSetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
            //_webDriver = new FirefoxDriver(); Question
        }

        [Test]
        public void Login()
        {
            _webDriver.Url = $"{BASE_URL}";

            _webDriver
                .FindElement(Layout.LoginLink)
                .Click();

            _webDriver
                .FindElement(LoginPage.UserNameInput)
                .SendKeys("Admin");

            _webDriver
                .FindElement(LoginPage.PasswordInput)
                .SendKeys("Admin");

            _webDriver
                .FindElement(LoginPage.LoginButton)
                .Click();

            var greetingsText = _webDriver
                .FindElement(By.CssSelector(".greetings"))
                .Text;

            Assert.That(greetingsText.Contains("Admin"));
        }

        public void Test2()
        {

        }

        [OneTimeTearDown]
        public void Tear()
        {
            _webDriver.Quit();
        }
    }
}
