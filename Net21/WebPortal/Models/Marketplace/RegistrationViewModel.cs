using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.marketplace
{
    public class MarketplaceRegistrationViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}