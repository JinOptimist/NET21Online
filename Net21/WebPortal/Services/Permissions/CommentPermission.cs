using WebPortal.DbStuff.Models;
using WebPortal.Enum;

namespace WebPortal.Services.Permissions
{
    public class CommentPermission : ICommentPermission
    {
        private AuthService _authService;

        public CommentPermission(AuthService authService)
        {
            _authService = authService;
        }

        public bool CanDelete(CommentEntity comment)
        {
            if (!_authService.IsAuthenticated())
            {
                return false;
            }

            var user = _authService.GetUser();

            if (comment.AuthorId == user.Id)
            {
                return true;
            }

            if (user.Role == Role.Admin || user.Role == Role.GrilModrator)
            {
                return true;
            }

            return false;
        }
    }
}
