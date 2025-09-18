using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.Enum;

namespace WebPortal.Models.Tourism
{
    public class LinkUsersWithRoleViewModel
    {
        public int AuthorId { get; set; }
        public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();

        public Role RoleId { get; set; }
        public List<SelectListItem> AllRoles { get; set; } = new List<SelectListItem>();
    }
}
