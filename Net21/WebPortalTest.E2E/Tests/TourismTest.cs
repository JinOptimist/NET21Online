using Microsoft.VisualBasic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Script;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WebPortalTest.E2E.Common;
using WebPortalTest.E2E.Helper;
using WebPortalTest.E2E.Selectors.Tourism;
using static System.Net.Mime.MediaTypeNames;

namespace WebPortalTest.E2E.Tests
{
    public class TourismTest
    {
        private IWebDriver _webDriver;

        [OneTimeSetUp]
        public void SetUp()
        {
            _webDriver = new ChromeDriver();
        }

        [Test]
        public void CreateTourTest()
        {
            LoginHelper.LoginAsAdmin(_webDriver);
            var testFilePath = @"C:\Users\user\source\repos\NET21Online\Net21\WebPortal\wwwroot\images\tourism\minsk.jpg";
            var toursIds = new List<string>();

            try
            {
                for (int i = 2; i < 4; i++)
                {
                    _webDriver.Url = "https://localhost:7210/Tourism/Shop";

                    var tourCountBefore = _webDriver
                   .FindElements(ShopPage.AllToursDivs)
                   .Count;

                    _webDriver
                        .FindElement(LayoutTourism.DropDownMenu)
                        .Click();

                    _webDriver
                        .FindElement(LayoutTourism.AddShopItem)
                        .Click();

                    _webDriver
                        .FindElement(TourAddPage.UserNameInput)
                        .SendKeys($"Test {i}");

                    _webDriver
                        .FindElement(TourAddPage.UploadImgFile)
                        .SendKeys(testFilePath);

                    _webDriver
                        .FindElement(TourAddPage.AddButton)
                        .Click();

                    Thread.Sleep(100);

                    var tourCountAfter = _webDriver
                        .FindElements(ShopPage.AllToursDivs)
                        .Count;

                    Assert.That(tourCountBefore == tourCountAfter - 1);

                    var tourId = _webDriver
                        .FindElements(ShopPage.AllToursDivs)
                        .Last()
                        .GetAttribute("data-id");

                    toursIds.Add(tourId);

                }
            }
            finally
            {
                foreach (var id in toursIds)
                {
                    try
                    {
                        _webDriver
                            .FindElement(ShopPage.GetTourDivByTourId(id))
                            .FindElement(ShopPage.RemoveButton)
                            .Click();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"There is a problem with {id}. Error message {e.Message}");
                    }

                }
            }
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _webDriver.Close();
        }
    }
}

