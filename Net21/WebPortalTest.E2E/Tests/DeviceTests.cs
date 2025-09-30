using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebPortalTest.E2E.Helper;
using WebPortalTest.E2E.Selectors;
using WebPortalTest.E2E.Selectors.CompShop;

namespace WebPortalTest.E2E.Tests
{
    public class DeviceTests
    {
        private IWebDriver _webDriver;

        [OneTimeSetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
        }

        //HELP
        //HELP
        //HELP
        //HELP
        //HELP
        [Test]
        public void Create100Devices()
        {
            LoginHelper.LoginAsAdmin(_webDriver);

            _webDriver 
                .FindElement(Layout.CompShopLink)
                .Click();

            _webDriver 
                .FindElement(LayoutCompShop.CatalogLink)
                .Click();

            var ids = new List<int>();

            try
            {
                for(int i = 1; i <= 10; i++)
                {
                    var allDeviceDivs = _webDriver
                        .FindElements(DevicePage.AllDeviceDivs)
                        .Count;

                    _webDriver.FindElement(LayoutCompShop.AddDeviceLink).Click();

                    _webDriver.FindElement(CompShopAddPage.AddDeviceName).SendKeys($"Mack Book_E2E {i}"); 
                    _webDriver.FindElement(CompShopAddPage.AddDeviceDescription).SendKeys($"Mack Book_E2E - Description");
                    _webDriver.FindElement(CompShopAddPage.AddDeviceImageUrl).SendKeys($"https://fk.by/uploads/images/cache/computers/kompyuter-bez-monitora-amd-a6_1-200x200.jpg");
                    _webDriver.FindElement(CompShopAddPage.AddDevicePrice).SendKeys($"{i}");

                    _webDriver
                        .FindElement(GirlAddPage.CreateButton)
                        .Click();

                    Thread.Sleep(100);

                    var deviceCountAfter = _webDriver
                        .FindElements(DevicePage.AllDeviceDivs)
                        .Count;

                    Assert.That(allDeviceDivs == deviceCountAfter + 1);

                    var deviceId = _webDriver
                        .FindElements(DevicePage.AllDeviceDivs)
                        .Last()
                        .GetAttribute("data-id");

                    ids.Add(int.Parse(deviceId));
                }
            }
            finally
            {
                foreach(var id in ids)
                {
                    _webDriver
                        .FindElement(LayoutCompShop.CatalogLink)
                        .Click();

                    _webDriver
                        .FindElement(DevicePage.GetDeviceDivById(id))
                        .FindElement(DevicePage.DeleteLink)
                        .Click();
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
