using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortalTest.E2E.Helper;
using WebPortalTest.E2E.Selectors;
using WebPortalTest.E2E.Selectors.CoffeeShop;


namespace WebPortalTest.E2E.Tests
{
    public class CoffeTest
    {
        public IWebDriver _driver;

        [OneTimeSetUp]
        public void Setup()
        {
            _driver = new FirefoxDriver();
        }

        [Test]
        public void AddNewCoffe()
        {
            LoginCoffeShopHelper.LoginAsModeratorCoffee(_driver);

            _driver.
                FindElement(Layout.CoffeLink)
                .Click();

            var coffeCountbefore = _driver
                .FindElements(CoffePage.AllCoffeDivs)
                 .Count;

            _driver
                .FindElement(LayoutCoffe.PageAdminCoffe)
                .Click();

            _driver
                .FindElement(LayoutCoffe.PageAddCoffe)
               .Click();

            _driver
                .FindElement(CoffeAddPage.ImgInput)
                .SendKeys("https://roast.by/upload/iblock/a40/k4fpevshxt0qxgdfchub7eatdqrix375.png");

            _driver
                .FindElement(CoffeAddPage.NameInput)
                .SendKeys("Arabicatest");

            var cellInput = _driver
                .FindElement(CoffeAddPage.CellInput);
            cellInput.Clear();
            cellInput.SendKeys("120");

            var authorSelect = new SelectElement(_driver.FindElement(CoffeAddPage.AuthorIdInput));
            authorSelect.SelectByValue("1");

            Thread.Sleep(500);

            _driver
                .FindElement(CoffeAddPage.CreateButton)
                .Click();

            var removeButtons = _driver.FindElements(By.CssSelector(".remove-coffe"));

            if (removeButtons.Count > 0)
            {
                var lastRemoveButton = removeButtons.Last();
                lastRemoveButton.Click();
            }
        }

        [OneTimeTearDown]
        public void Tear()
        {
            _driver.Quit();
        }
    }
}
