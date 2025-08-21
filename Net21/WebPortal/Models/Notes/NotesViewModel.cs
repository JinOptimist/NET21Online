namespace WebPortal.Models.Notes;

public class NotesViewModel
{
    public List<CategoryViewModel> Categories { get; set; }
    public List<TagViewModel> Tags { get; set; }
    public List<NoteViewModel> Notes { get; set; }
    public List<BannerViewModel> Banners { get; set; }
}