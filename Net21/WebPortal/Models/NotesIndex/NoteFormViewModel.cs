using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortal.Models.NotesIndex;

public class NoteFormViewModel
{
    public string Title { get; set; }
    public string? ImageUrl { get; set; }
    public string Description { get; set; }
    public int? CategoryId { get; set; }
    public List<int> TagIds { get; set; } = new();
    [ValidateNever]
    public SelectList CategoryList { get; set; }
    [ValidateNever]
    public MultiSelectList TagList { get; set; }
}