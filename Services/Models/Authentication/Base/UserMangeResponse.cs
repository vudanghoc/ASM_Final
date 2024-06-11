namespace Services.Models.Authentication.Base
{
    public class UserMangeResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string RefreshToken { get; set; }
    }
}
