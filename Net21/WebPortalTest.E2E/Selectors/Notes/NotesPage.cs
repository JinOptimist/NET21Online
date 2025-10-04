using OpenQA.Selenium;

namespace WebPortalTest.E2E.Selectors.Notes;

public class NotesPage
{
    public static By AllNoteDivs => By.CssSelector(".right_side .note");
    public static By GetNoteDeleteLink => By.CssSelector("a.note-delete");
    
    public static By GetNoteDivById(string id)
        => By.CssSelector($".right_side .note[data-id='{id}']");
}