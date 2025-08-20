using Microsoft.AspNetCore.Mvc.Rendering;
using UnderTheBridge.Data.Models;
using WebPortal.DbStuff.Models;

namespace WebPortal.Models.UnderTheBridge
{
    public class RelinkCommentViewModel
    {
        public int UserId { get; set; }
        public List<SelectListItem> AllUsers { get; set; } = new List<SelectListItem>();
        public int CommentId { get; set; }
        public List<SelectListItem> AllComments { get; set; } = new List<SelectListItem>();
    }
}
