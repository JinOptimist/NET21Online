using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebPortal.Enum;
using WebPortal.Services;

namespace WebPortal.Controllers.CustomAuthorizeAttributes;

public class RoleNotesAttribute : ActionFilterAttribute
{
    private List<NotesUserRole> _availableRoles;

    public RoleNotesAttribute(NotesUserRole notesUserRole)
    {
        _availableRoles = new List<NotesUserRole> { notesUserRole };
    }

    public RoleNotesAttribute(params NotesUserRole[] notesUserRole)
    {
        _availableRoles = notesUserRole.ToList();
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var authNotesService = context
            .HttpContext
            .RequestServices
            .GetRequiredService<AuthNotesService>();

        if (!authNotesService.IsAuthenticated())
        {
            context.Result = ((Controller)context.Controller)
                .RedirectToAction("Login", "AuthNotes");
            return;
        }

        var userRole = authNotesService.GetRole();
        if (!_availableRoles.Contains(userRole))
        {
            context.Result = ((Controller)context.Controller)
                .RedirectToAction("Forbid", "AuthNotes");
            return;
        }

        base.OnActionExecuting(context);
    }
}