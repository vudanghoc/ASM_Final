using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Services;
using Services.Models.Authentication;
using Services.Models.Authentication.Base;

namespace WebAPI.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private IAuthenticationService _userService;
        public AuthenticationController(IAuthenticationService userService)
        {
            _userService = userService;
        }

        // /api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterForView model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUser(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginForView model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUser(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenForView model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RefreshToken(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }
    }
}
