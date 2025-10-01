using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebPortalTest.E2E.Helper;
using WebPortalTest.E2E.Selectors.Notes;

namespace WebPortalTest.E2E.Tests;

public class NotesTests
{
    private IWebDriver _webDriver;

    [OneTimeSetUp]
    public void Setup()
    {
        _webDriver = new ChromeDriver();
    }

    [Test]
    public void Create20Notes()
    {
        LoginNotesHelper.LoginAsAdmin(_webDriver);

        var ids = new List<string>();

        try
        {
            for (int i = 0; i < 20; i++)
            {
                var notesCountBefore = _webDriver
                    .FindElements(NotesPage.AllNoteDivs)
                    .Count;
                
                _webDriver
                    .FindElement(LayoutNotes.AddNoteLink)
                    .Click();
                
                _webDriver
                    .FindElement(NoteAddPage.TitleInput)
                    .SendKeys($"Note {i}");
                
                _webDriver
                    .FindElement(NoteAddPage.DescriptionInput)
                    .SendKeys("Test Description");
                
                _webDriver
                    .FindElement(NoteAddPage.ImageUrlInput)
                    .SendKeys("images/notes/csharp.jpeg");
                
                var categorySelect = _webDriver.FindElement(NoteAddPage.CategoryIdSelect);
                categorySelect.Click();
                
                var option = categorySelect.FindElement(NoteAddPage.CategoryIdOptionProgramming);
                option.Click();
                
                var tagSelect = _webDriver.FindElement(NoteAddPage.TagIdsSelectMultiple);
                var optionCSharp = tagSelect.FindElement(NoteAddPage.TagIdsSelectMultipleOptionCSharp);
                optionCSharp.Click();
                var optionNew = tagSelect.FindElement(NoteAddPage.TagIdsSelectMultipleOptionNew);
                optionNew.Click();
                
                _webDriver.FindElement(NoteAddPage.CreateButton).Click();
                
                var wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(500));
                wait.Until(driver =>
                {
                    var notesAfter = driver.FindElements(NotesPage.AllNoteDivs).Count;
                    return notesAfter > notesCountBefore;
                });
                
                var notesCountAfter = _webDriver
                    .FindElements(NotesPage.AllNoteDivs)
                    .Count;
                
                Assert.That(notesCountAfter, Is.EqualTo(notesCountBefore + 1));
                
                var newNote = _webDriver.FindElements(NotesPage.AllNoteDivs).Last();
                var noteId = newNote.GetAttribute("data-id");
                ids.Add(noteId);
            }
        }
        finally
        {
            foreach (var id in ids)
            {
                _webDriver
                    .FindElement(NotesPage.GetNoteDivById(id))
                    .FindElement(NotesPage.GetNoteDeleteLink)
                    .Click();
            }
        }
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _webDriver.Close();
    }
}