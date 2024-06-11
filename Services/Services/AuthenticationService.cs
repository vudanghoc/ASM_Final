using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Services.Contracts.Services;
using Services.Models.Authentication;
using Services.Models.Authentication.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private IConfiguration _configuration;
        private UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AuthenticationService(UserManager<AppUser> userManager,
          IConfiguration configuration,
          SignInManager<AppUser> signInManager,
          ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<UserMangeResponse> LoginUser(LoginForView model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new UserMangeResponse
                {
                    Message = $"Làm gì có cái email nào như lày {model.Email}",
                    IsSuccess = false,
                };
            }
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return new UserMangeResponse
                {
                    Message = $"Password của {model.Email} sai cmnr :v",
                    IsSuccess = false,
                };
            }

            // create claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id)
            };
            var roles = await _signInManager.UserManager.GetRolesAsync(user);
            // add role to claims
            if (roles.Any())
            {
                var roleClaim = string.Join(",", roles);
                claims.Add(new Claim(ClaimTypes.Role, roleClaim));
            }
            //call token
            var token = CreateToken(claims);
            var refreshToken = GenerateRefreshToken();

            _ = int.TryParse(_configuration["AuthSettings:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            //DateTime refreshDate = DateTime.Now.AddDays(refreshTokenValidityInDays);
            DateTime refreshDate = DateTime.Now;
            user.RefreshTokenExpiryTime = refreshDate.ToUniversalTime();

            if (user.LastLoggedInDate == null)
            {
                Console.WriteLine("Chào người mới nhá");
            }

            DateTime now = DateTime.Now;
            user.LastLoggedInDate = now.ToUniversalTime();

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return new UserMangeResponse
                {
                    Message = "Failed to update user last login date.",
                    IsSuccess = false
                };
            }

            return new UserMangeResponse
            {
                Message = new JwtSecurityTokenHandler().WriteToken(token),
                IsSuccess = true,
                RefreshToken = refreshToken,
                ExpireDate = token.ValidTo
            };
        }
        public async Task<UserMangeResponse> RegisterUser(RegisterForView model)
        {

            if (model == null)
            {
                throw new NullReferenceException("Register Model is null");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return new UserMangeResponse
                {
                    Message = "Confirm pass doesn't match the pass",
                    IsSuccess = false
                };
            }

            var isMailExisted = await _userManager.FindByEmailAsync(model.Email);
            if (isMailExisted != null)
            {
                return new UserMangeResponse
                {
                    Message = "This email already exists",
                    IsSuccess = false
                };
            }

            var user = CreateUser();
            {
                user.Email = model.Email;
                user.UserName = model.Email;
                user.Name = model.FullName;
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return new UserMangeResponse
                {
                    Message = "User created successfully",
                    IsSuccess = true
                };
            }
            return new UserMangeResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
        public async Task<UserMangeResponse> ForgotPassword(string mail)
        {
            var user = await _userManager.FindByIdAsync(mail);
            if (user == null)
            {
                return new UserMangeResponse
                {
                    Message = "No user found",
                    IsSuccess = false
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return null;
        }
        public Task<UserMangeResponse> ConfirmEmailAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }
        public async Task<NoContentResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }
            return null;
        }
        public async Task<UserMangeResponse> RefreshToken(TokenForView model)
        {
            if (model == null)
            {
                return new UserMangeResponse
                {
                    Message = "Invalid client request",
                    IsSuccess = false
                };
            }
            string? accessToken = model.AccessToken;
            string? refreshToken = model.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return new UserMangeResponse
                {
                    Message = "Invalid access token or refresh token",
                    IsSuccess = false
                };
            }
            string email = principal.Identity.Name;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new UserMangeResponse
                {
                    Message = "Invalid access token or refresh token",
                    IsSuccess = false
                };
            }
            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new UserMangeResponse
            {
                Message = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }
        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                    $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private JwtSecurityToken CreateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));
            var add30Date = DateTime.Now.AddDays(30);

            var token = new JwtSecurityToken(
                 issuer: _configuration["AuthSettings:Issuer"],
                 audience: _configuration["AuthSettings:Audience"],
                 claims: claims,
                 expires: add30Date.ToUniversalTime(),
                 signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                 );
            return token;
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
