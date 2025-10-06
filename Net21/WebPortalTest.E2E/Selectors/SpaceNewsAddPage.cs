using OpenQA.Selenium;


namespace WebPortalTest.E2E.Selectors
{
    public class SpaceNewsAddPage
    {
        public static By ImageUrlInput => By.CssSelector("[name=imageUrl]");
        public static By TitleInput => By.CssSelector("[name=title]");
        public static By ContentInput => By.CssSelector("[name=content]");
        public static By AuthorIdDropdown => By.CssSelector("[name=AuthorId]");
        public static By FirstAuthorOption => By.CssSelector("[name=AuthorId] option:not([value=''])");
        public static By SubmitButton => By.CssSelector("form button[type=submit]");
    }
}

