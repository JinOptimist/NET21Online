using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPortal.Models.Users
{
    public class SetRoleUsersViewModel
    {
        public int UserId { get; set; }
        public List<SelectListItem>? AllUsers { get; set; } = new List<SelectListItem>();

        public int RoleId { get; set; }
        public List<SelectListItem> AllRoles { get; set; } = new List<SelectListItem>();
    }
}
