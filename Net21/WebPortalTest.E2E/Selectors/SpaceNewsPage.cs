using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortalTest.E2E.Selectors
{
    public class SpaceNewsPage
    {
        public static By AllNewsItems => By.CssSelector(".news-item");
        public static By LastNewsItem => By.CssSelector(".news-item:last");
        public static By NewsTitle => By.CssSelector("h3");
        public static By NewsContent => By.CssSelector("p");
        public static By DeleteButton => By.CssSelector("form button[type=submit]");

        public static By GetNewsItemByNewsId(string id)
            => By.CssSelector($".news-text[data-id='{id}']");
    }
}
