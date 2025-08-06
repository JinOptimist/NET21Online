using WebPortal.Models.NotesIndex;

namespace WebPortal.Models;

public class NotesIndexViewModel
{
    public List<CategoryViewModel> Categories { get; set; }
    public List<TagViewModel> Tags { get; set; }
    public List<NoteViewModel> Notes { get; set; }
    public List<BannerViewModel> Banners { get; set; }
}