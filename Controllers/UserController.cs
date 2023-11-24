using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Backend.Data;
using Backend.Models;
using Backend.Repositories;
using Backend.Services;
using Backend.DTO;
using Twilio.Rest.Api.V2010.Account;
using Twilio;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private TrangTraiContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _config;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly UserService _userService;

        public UserController(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager, TrangTraiContext dbContext, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, IConfiguration config, UserService userService)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _userManager = userManager;
            _config = config;
            _userService = userService;
        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserLoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return BadRequest("Không tồn tại tài khoản trong hệ thống");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return BadRequest("Tài khoản hoặc mật khẩu không chính xác");
            }

            var accessToken = await _userService.GenerateJSONWebToken(user);
            return Ok(new { AccessToken = accessToken });
        }


        [HttpPost("Logout")]
        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterRequest model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Mật khẩu và mật khẩu nhập lại không trùng nhau");
            }
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            if (!Regex.IsMatch(model.Password, passwordPattern))
            {
                return BadRequest("Mật khẩu tối thiểu 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt");
            }

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest("Tài khoản đã tồn tại");
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                DistrictId = model.DistrictId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                NormalizedEmail = model.Email.ToUpper(),
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var userId = await _userManager.GetUserIdAsync(user);

                return Ok("Đăng ký tài khoản thành công.");
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(errors);
            }
        }

        [HttpPost("ForgotPassword")]

        public async Task<ActionResult> ForgotPassword(string phoneNumber)
        {
            var user = _userManager.Users.FirstOrDefault(user => user.PhoneNumber == phoneNumber);

            if (user != null)
            {
                Random random = new Random();
                int code = random.Next(100000, 999999);

                var tokenResetPass = await _userManager.GeneratePasswordResetTokenAsync(user);

                string accountSid = "AC83f758deead35d6340437ea92e287852";
                string authToken = "4754595bd737e195a8091a88e8eb3612";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "<#> Your code is " + code + " 9i/S9FS/SJu",
                    from: new Twilio.Types.PhoneNumber("+16562231599"),
                    to: new Twilio.Types.PhoneNumber("+84" + phoneNumber)
                );

                Console.WriteLine(message.Sid);

                await _dbContext.Users
                .Where(u => u.PhoneNumber == phoneNumber)
                .ExecuteUpdateAsync(b => b.SetProperty(u => u.Code, code));

                return Ok(tokenResetPass);
            }
            else
            {
                return BadRequest("Không tồn tại tài khoản");
            }
        }

        [HttpPost("VerifyOTP")]
        public async Task<ActionResult> VerifyOTP(int code)
        {
            var user = _userManager.Users.FirstOrDefault(user => user.Code == code);

            if (user != null)
            {
                return Ok();
            }
            return BadRequest("Mã OTP không đúng");
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] UserChangePasswordReq model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.TokenResetPass, model.NewPassword);

                if (result.Succeeded)
                {
                    return Ok("Đổi mật khẩu thành công");
                }
                return BadRequest("Đổi mật khẩu thất bại");
            }
            return BadRequest("Tài khoản không tồn tại");
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            if (user == null)
            {
                return BadRequest("Không tìm thấy tài khoản");
            }
            else
            {
                var result = await _signInManager.UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest();
            }

        }
    }
}
