namespace Services.Models.Authentication.Base
{
    public class TokenForView
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
