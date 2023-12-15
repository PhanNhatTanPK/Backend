using Backend.Data;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Services
{
    public class UserService
    {
        private TrangTraiContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        //private readonly EmailServices _emailServices;

        public UserService(IUnitOfWork unitOfWork,
                        SignInManager<ApplicationUser> signInManager,
                        TrangTraiContext dbContext,
                        UserManager<ApplicationUser> userManager,
                        IConfiguration config,
                        IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _userManager = userManager;
            _config = config;
            _emailSender = emailSender;
        }
        public async Task<string> GenerateJSONWebToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var exprires = int.Parse(_config["Jwt:Expires"]);
            //var claims = _userManager.GetClaimsAsync(user).Result;

            //claims.Add(new Claim("Id", user.Id));
            //claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            //claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //claims.Add(new Claim("Roles", Guid.NewGuid().ToString()));
            var claims = await GetValidClaims(user);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(exprires),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<List<Claim>> GetValidClaims(ApplicationUser user)
        {
            IdentityOptions _options = new IdentityOptions();

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("Id", user.Id),
                //new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName),
                new Claim("PhoneNumber", user.PhoneNumber),
                new Claim("DistrictId", user.DistrictId.ToString())
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                //var role = await _roleManager.FindByNameAsync(userRole);
                //if (role != null)
                //{
                //    var roleClaims = await _roleManager.GetClaimsAsync(role);
                //    foreach (Claim roleClaim in roleClaims)
                //    {
                //        claims.Add(roleClaim);
                //    }
                //}
            }
            return claims;
        }
    }
}
