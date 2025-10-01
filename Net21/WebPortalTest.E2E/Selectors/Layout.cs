using OpenQA.Selenium;

namespace WebPortalTest.E2E.Selectors
{
    public class Layout
    {
        public static By LoginLink => By.CssSelector(".login");

        public static By GirlLink => By.CssSelector(".girl-link");

        public static By SpaceStation => By.CssSelector(".spacestation-link");
    }
}
