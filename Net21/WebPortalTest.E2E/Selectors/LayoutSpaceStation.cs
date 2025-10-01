using OpenQA.Selenium;

namespace WebPortalTest.E2E.Selectors
{
    public class LayoutSpaceStation
    {
        public static By NewsLink => By.CssSelector("a[href*='/SpaceStation/News']");
        public static By MainLink => By.CssSelector("a[href*='/SpaceStation']");
    }
}

