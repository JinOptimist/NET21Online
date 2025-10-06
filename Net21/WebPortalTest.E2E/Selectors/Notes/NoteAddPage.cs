using OpenQA.Selenium;

namespace WebPortalTest.E2E.Selectors.Notes;

public class NoteAddPage
{
    public static By TitleInput => By.XPath("//input[@id='Title']");
    public static By DescriptionInput => By.XPath("//textarea[@id='Description']");
    public static By ImageUrlInput => By.XPath("//input[@id='ImageUrl']");
    public static By CategoryIdSelect => By.XPath("//select[@id='CategoryId']");
    public static By CategoryIdOptionProgramming => By.XPath(".//option[normalize-space(text())='Programming']");
    public static By TagIdsSelectMultiple => By.XPath("//select[@id='TagIds']");
    public static By TagIdsSelectMultipleOptionCSharp => By.XPath(".//option[text()='#CSharp']");
    public static By TagIdsSelectMultipleOptionNew => By.XPath(".//option[text()='#NEW']");
    public static By CreateButton => By.XPath("//button[@type='submit']");
}