using OpenQA.Selenium;

namespace WebPortalTest.E2E.Selectors.Notes;

public class LoginPageNotes
{
    public static By UserNameInput => By.Id("UserName");
    public static By PasswordInput => By.Id("Password");
    public static By LoginButton => By.CssSelector(".custom_input button.custom_input_button");
}