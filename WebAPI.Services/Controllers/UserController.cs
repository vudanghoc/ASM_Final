using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Services;

namespace WebAPI.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpPut("AddUserRole")]
        public async Task<ActionResult> UpdateUserRole(string userName, [FromBody] string newRole)
        {
            var result = await _userService.UpdateUserRole(userName, newRole);
            if (result)
            {
                return Ok("Role updated successfully.");
            }
            else
            {
                return BadRequest("Failed to update role.");
            }
        }
        [HttpGet("roles")]
        public async Task<ActionResult<List<string>>> GetAllRoles()
        {
            var roles = await _userService.GetAllRolesAsync();
            return Ok(roles);
        }
    }
}