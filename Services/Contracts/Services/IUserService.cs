using Services.Models.User;

namespace Services.Contracts.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserForView>> GetAllUsersAsync();
        Task<List<string>> GetAllRolesAsync();
        Task<bool> UpdateUserRole(string userName, string userRole);

    }
}
