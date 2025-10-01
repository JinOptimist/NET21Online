using OpenQA.Selenium;

namespace WebPortalTest.E2E.Selectors.Notes;

public class LayoutNotes
{
    public static By LoginLink => By.CssSelector(".auth-links a:nth-child(1)");
    public static By AddNoteLink => By.CssSelector(".header-right .create-note-btn");
}