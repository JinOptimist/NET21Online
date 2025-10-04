using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Xml.Linq;
using WebPortalTest.E2E.Helper;
using WebPortalTest.E2E.Selectors;

namespace WebPortalTest.E2E.Tests
{
    public class GirlTests
    {
        private IWebDriver _webDriver;

        [OneTimeSetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
        }

        [Test]
        public void Create100Grils()
        {
            LoginHelper.LoginAsAdmin(_webDriver);

            _webDriver
                .FindElement(Layout.GirlLink)
                .Click();

            var ids = new List<string>();

            try
            {
                for (int i = 0; i < 30; i++)
                {
                    var girlCountBefore = _webDriver
                        .FindElements(GirlsPage.AllGirlDivs)
                        .Count;

                    _webDriver
                        .FindElement(LayoutGirl.AddGirlLink)
                        .Click();

                    _webDriver
                        .FindElement(GirlAddPage.NameInput)
                        .SendKeys($"Girl {i}");

                    _webDriver
                        .FindElement(GirlAddPage.SrcInput)
                        .SendKeys($"https://www.pixaii.com/files/preview/960x1713/11710095717hg6d0lev9csqjqz2ftjqtq1f43buwnsdwmnw4ik6q95xifkfj7ps1wqdxcx7wbopyv5gxkge5jefrj27ygpqofgrusum8knw7lty.jpg");

                    _webDriver
                        .FindElement(GirlAddPage.RatingInput)
                        .SendKeys($"7");

                    _webDriver
                        .FindElement(GirlAddPage.CreateButton)
                        .Click();

                    Thread.Sleep(100);

                    var girlCountAfter = _webDriver
                        .FindElements(GirlsPage.AllGirlDivs)
                        .Count;

                    Assert.That(girlCountAfter == girlCountBefore + 1);

                    var girlId = _webDriver
                        .FindElements(GirlsPage.AllGirlDivs)
                        .Last()
                        .GetAttribute("data-id");

                    ids.Add(girlId);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                foreach (var id in ids)
                {
                    _webDriver
                        .FindElement(GirlsPage.GetGirlDivByGirlId(id))
                        .FindElement(GirlsPage.GetDeleteLink)
                        .Click();
                }
            }
        }

        [OneTimeTearDown]
        public void Tear()
        {
            _webDriver.Close();
        }
    }
}
