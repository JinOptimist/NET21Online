using OpenQA.Selenium;

namespace WebPortalTest.E2E.Selectors
{
    public class GirlAddPage
    {
        public static By NameInput => By.CssSelector("[name=Name]");
        public static By SrcInput => By.CssSelector("[name=Src]");
        public static By RatingInput => By.CssSelector("[name=Rating]");
        public static By AuthorIdInput => By.CssSelector("[name=AuthorId]");
        public static By CreateButton => By.CssSelector("form button");
    }
}
