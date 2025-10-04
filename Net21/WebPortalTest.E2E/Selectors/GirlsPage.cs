using OpenQA.Selenium;

namespace WebPortalTest.E2E.Selectors
{
    public class GirlsPage
    {
        public static By AllGirlDivs => By.CssSelector(".girls .girl");
        public static By LastGirlDiv => By.CssSelector(".girls .girl:last");

        public static By GetDeleteLink => By.CssSelector("a.delete");

        public static By GetGirlDivByGirlId(string id)
            => By.CssSelector($".girls .girl[data-id='{id}']");
    }
}
