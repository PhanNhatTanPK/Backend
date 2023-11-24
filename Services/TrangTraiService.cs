using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.IdentityModel.Tokens.Jwt;
using Backend.Models;
using Backend.Data;

namespace Backend.Services
{
    public class TrangTraiService
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmailSender _emailSender;
		private readonly IConfiguration _configuration;
		private TrangTraiContext _dbContext;

		public TrangTraiService(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IConfiguration configuration, TrangTraiContext dbContext)
		{
			_userManager = userManager;
			_emailSender = emailSender;
			_configuration = configuration;
			_dbContext = dbContext;
		}

        public decimal CoordinateToDecimal(string coordinate)
        {
            var degree = Convert.ToDecimal(coordinate.Split("°")[0]);
            var minutes = Convert.ToDecimal(coordinate.Split("'")[0].Split("°")[1]);
            var seconds = Convert.ToDecimal(coordinate.Split("'")[1]);

            return degree + (minutes / 60) + (seconds / 3600);
        }

        public long GetUserInfo(string username)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.UserName == username);

                if (user != null)
                {
                    return (long)user.DistrictId;
                }
                else
                {
                    throw new InvalidOperationException("User not found");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while processing the request.", ex);
            }
        }

        public string GetBearerToken(string authorizationHeader)
        {
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return null;
            }

            return authorizationHeader.Substring("Bearer ".Length);
        }
        public string GetInfo(string token)
        {
            var jwttoken = token;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwttoken);

            var userName = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
            return userName;
        }
    }
}
