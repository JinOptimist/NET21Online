namespace WebPortal.Models.Auth
{
    public class AuthViewModel
    {
        public string UserName {  get; set; }
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
