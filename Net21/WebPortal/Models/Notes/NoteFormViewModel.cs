using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.Models.CustomValidationAttributtes;
using WebPortal.Models.CustomValidationAttributtes.Notes;

namespace WebPortal.Models.Notes;

public class NoteFormViewModel
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(150, ErrorMessage = "Title cannot exceed 150 characters")] 
    public string Title { get; set; }
    [IsCorrectImageUrl(ErrorMessage = "Must be a valid image URL (.jpg, .png, .gif)")]
    public string? ImageUrl { get; set; }
    [Required(ErrorMessage = "Description is required")]
    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string Description { get; set; }
    public int? CategoryId { get; set; }
    [MustHaveNewTag]
    public List<int> TagIds { get; set; } = new();
    [ValidateNever]
    public SelectList CategoryList { get; set; }
    [ValidateNever]
    public MultiSelectList TagList { get; set; }
    public int AuthorId { get; set; }
    public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();
}