using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortalTest.E2E.Helper;

namespace WebPortalTest.E2E.Tests
{
    public class SpaceNewsTests
    {
        private IWebDriver _webDriver;

        [OneTimeSetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
        }

        [Test]
        public void CreateSpaceNews()
        {
            LoginHelper.LoginAsAdmin(_webDriver);

            _webDriver.Url = "https://localhost:7210/SpaceStation";

            _webDriver
                .FindElement(By.CssSelector("a[href*='/SpaceStation/News']"))
                .Click();

            _webDriver
                .FindElement(By.CssSelector("[name=imageUrl]"))
                .SendKeys("https://upload.wikimedia.org/wikipedia/commons/thumb/0/09/Mir_Space_Station_viewed_from_Endeavour_during_STS-89.jpg/500px-Mir_Space_Station_viewed_from_Endeavour_during_STS-89.jpg");

            _webDriver
                .FindElement(By.CssSelector("[name=title]"))
                .SendKeys("Test Space News Title");

            _webDriver
                .FindElement(By.CssSelector("[name=content]"))
                .SendKeys("This is test content for space news created by E2E test.");

            var authorDropdown = _webDriver.FindElement(By.CssSelector("[name=AuthorId]"));
            var firstAuthorOption = authorDropdown.FindElement(By.CssSelector("option:not([value=''])"));
            firstAuthorOption.Click();

            _webDriver
                .FindElement(By.CssSelector("form button[type=submit]"))
                .Click();

            Thread.Sleep(500);

            var newsItems = _webDriver.FindElements(By.CssSelector(".news-item"));
            var lastNewsItem = newsItems.Last();

            var newsTitle = lastNewsItem.FindElement(By.CssSelector("h3")).Text;
            Assert.That(newsTitle.Contains("Test Space News Title"));

            var newsContent = lastNewsItem.FindElement(By.CssSelector("p")).Text;
            Assert.That(newsContent.Contains("This is test content for space news"));

            var deleteButton = lastNewsItem.FindElement(By.CssSelector("form button[type=submit]"));
            deleteButton.Click();

            Thread.Sleep(200);
        }

        [Test]
        public void CreateMultipleSpaceNews()
        {
            LoginHelper.LoginAsAdmin(_webDriver);

            var newsIds = new List<string>();

            try
            {
                for (int i = 0; i < 5; i++)
                {
                    _webDriver.Url = "https://localhost:7210/SpaceStation/News";

                    _webDriver
                        .FindElement(By.CssSelector("[name=imageUrl]"))
                        .SendKeys($"https://upload.wikimedia.org/wikipedia/commons/thumb/0/09/Mir_Space_Station_viewed_from_Endeavour_during_STS-89.jpg/500px-Mir_Space_Station_viewed_from_Endeavour_during_STS-89.jpg");

                    _webDriver
                        .FindElement(By.CssSelector("[name=title]"))
                        .SendKeys($"Space News {i} - Test Title");

                    _webDriver
                        .FindElement(By.CssSelector("[name=content]"))
                        .SendKeys($"This is test content for space news #{i} created by E2E test.");

                    var authorDropdown = _webDriver.FindElement(By.CssSelector("[name=AuthorId]"));
                    var firstAuthorOption = authorDropdown.FindElement(By.CssSelector("option:not([value=''])"));
                    firstAuthorOption.Click();

                    _webDriver
                        .FindElement(By.CssSelector("form button[type=submit]"))
                        .Click();

                    Thread.Sleep(300);

                    var newsItems = _webDriver.FindElements(By.CssSelector(".news-item"));
                    var lastNewsItem = newsItems.Last();
                    var newsTextElement = lastNewsItem.FindElement(By.CssSelector(".news-text"));
                    var newsId = newsTextElement.GetAttribute("data-id");

                    if (!string.IsNullOrEmpty(newsId))
                    {
                        newsIds.Add(newsId);
                    }
                }
            }
            finally
            {
                foreach (var newsId in newsIds)
                {
                    try
                    {
                        var newsElement = _webDriver.FindElement(By.CssSelector($".news-text[data-id='{newsId}']"));
                        var deleteButton = newsElement.FindElement(By.CssSelector("form button[type=submit]"));
                        deleteButton.Click();
                        Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting news {newsId}: {ex.Message}");
                    }
                }
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _webDriver.Close();
        }
    }
}
