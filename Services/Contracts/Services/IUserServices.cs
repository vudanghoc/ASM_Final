using Services.Models.Authentication.Base;
using Services.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Services.Contracts.Services
{
    public interface IUserServices
    {
        Task<UserMangeResponse> RegisterUser(RegisterForView model);
        Task<UserMangeResponse> LoginUser(LoginForView model);
        Task<UserMangeResponse> ForgotPassword(string mail);
        Task<UserMangeResponse> ConfirmEmailAsync(string userId, string token);
        Task<NoContentResult> RevokeAll();
        Task<UserMangeResponse> RefreshToken(TokenForView model);
    }
}
