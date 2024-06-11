using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTimeOffset? LastLoggedInDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
        //public ICollection<Cart>? Carts { get; set; }
    }
}
