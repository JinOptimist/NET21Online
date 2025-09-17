using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebPortal.Enum;
using WebPortal.Services;

namespace WebPortal.Controllers.CustomAuthorizeAttributes
{
    public class RoleAttribute : ActionFilterAttribute
    {
        private List<Role> _availableRoles;

        public RoleAttribute(Role role)
        {
            _availableRoles = new List<Role> { role };
        }

        public RoleAttribute(params Role[] role)
        {
            _availableRoles = role.ToList();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var authService = context
                .HttpContext
                .RequestServices
                .GetRequiredService<IAuthService>();

            if (!authService.IsAuthenticated())
            {
                context.Result = ((Controller)context.Controller)
                    .RedirectToAction("Login", "Auth");
                return;
            }

            var userRole = authService.GetRole();
            if (!_availableRoles.Contains(userRole))
            {
                context.Result = ((Controller)context.Controller)
                    .RedirectToAction("Forbid", "Auth");
                // context.Result = new ForbidResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
