using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.Services;
using Services.Models.User;

namespace Services.Services
{
    public class UserService: IUserService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<UserForView>> GetAllUsersAsync()
        {
            IEnumerable<AppUser> users = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserForView>>(users); ;
        }
        public async Task<List<string>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(role => role.Name).ToList();
        }
        public async Task<bool> UpdateUserRole(string userName, string userRole)
        {
            var user = await _userManager.FindByEmailAsync(userName);
            if (user == null)
            {
                return false;
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeRolesResult.Succeeded)
            {
                return false;
            }

            var addRoleResult = await _userManager.AddToRoleAsync(user, userRole);
            if (!addRoleResult.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}