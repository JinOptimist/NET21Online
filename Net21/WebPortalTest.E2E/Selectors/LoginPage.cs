using OpenQA.Selenium;

namespace WebPortalTest.E2E.Selectors
{
    public class LoginPage
    {
        public static By UserNameInput => By.CssSelector("[name=UserName]");

        public static By PasswordInput => By.CssSelector("[name=Password]");

        public static By LoginButton => By.CssSelector("form button");
    }
}
