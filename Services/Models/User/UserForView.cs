namespace Services.Models.User
{
    public class UserForView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTimeOffset? LastLoggedInDate { get; set; }
    }
}
