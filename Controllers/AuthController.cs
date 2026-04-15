using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EkartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authService;
        private readonly IEmailInterface _emailInterface;

        public AuthController(IAuthRepository authService, IEmailInterface emailInterface)
        {
            _authService = authService;
            _emailInterface = emailInterface;
        }

        [HttpPost("loginFK")]
        public async Task<IActionResult> LoginFK([FromForm] AuthFKResponseModel login)
        {
            var result = await _authService.LoginFK(login.email, login.password);
            return ProcessLoginResult(result, login.email);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginUserDTO login)
        {
            var result = await _authService.AuthenticateUser(login.Email, login.Password);
            return ProcessLoginResult(result, login.Email);
        }

        private IActionResult ProcessLoginResult(IActionResult result, string email)
        {
            if (result is OkObjectResult okResult && okResult.Value is object value)
            {
                var json = JObject.FromObject(value);
                var token = json.GetValue("token")?.ToString();
                var message = json.GetValue("message")?.ToString();

                if (!string.IsNullOrEmpty(message))
                {
                    return message switch
                    {
                        "User hasn't set their password yet" => Ok(new { code = 400, mess = "Password user" }),
                        "Unknown User" => Ok(new { code = 450, mess = "Unknown User" }),
                        _ => BadRequest("Invalid login credentials")
                    };
                }

                if (!string.IsNullOrEmpty(token))
                {
                    var userClaims = GetUserClaims(token);
                    return Ok(new
                    {
                        Email = email,
                        Token = token,
                        RoleId = userClaims.RoleId,
                        UserName = userClaims.UserName,
                        UserId = userClaims.UserId
                    });
                }
            }
            return BadRequest("Invalid login credentials");
        }

        private (string UserId, string UserName, string RoleId) GetUserClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            return (
                UserId: jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value,
                UserName: jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value,
                RoleId: jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value
            );
        }

        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] RequestPasswordResetDTO request)
        {
            return await _authService.RequestPasswordReset(request.Email);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDTO request)
        {
            return await _authService.VerifyOtp(request.Email, request.Otp);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO request)
        {
            return await _authService.ResetPassword(request.Email, request.password, request.password_confirmation);
        }

    }
}