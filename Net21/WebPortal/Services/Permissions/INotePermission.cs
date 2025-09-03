using WebPortal.DbStuff.Models.Notes;

namespace WebPortal.Services.Permissions;

public interface INotePermission
{
    bool IsAllowedToDelete(Note note);
    bool IsAllowedToEdit(Note note);
}