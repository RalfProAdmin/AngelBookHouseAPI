using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EkartAPI.Repository.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly EkartDBcontext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IEmailInterface _emailInterface;

        public AuthRepository(EkartDBcontext dbContext, IConfiguration configuration, IEmailInterface emailInterface)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _emailInterface = emailInterface;
        }

        public async Task<IActionResult> LoginFK(string email, string password)
        {
            var user = await Task.Run(() => _dbContext.tbl_users.FirstOrDefault(u => u.Email == email));
            return ValidateUser(user, password);
        }

        public async Task<IActionResult> AuthenticateUser(string email, string password)
        {
            var user = await Task.Run(() => _dbContext.tbl_users.FirstOrDefault(u => u.Email == email));
            return ValidateUser(user, password);
        }

        private IActionResult ValidateUser(userModel user, string password)
        {
            if (user == null) return new UnauthorizedResult();

            if (string.IsNullOrEmpty(user.Password))
                return new BadRequestObjectResult(new { message = "User hasn't set their password yet" });

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                return new UnauthorizedResult(); // Invalid password

            var token = GenerateJwtToken(user);
            return new OkObjectResult(new { token });
        }

        private string GenerateJwtToken(userModel user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
{
    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // Ensure this matches what you're looking for
    new Claim("userId", user.UserId.ToString()), // Explicitly add userId claim
    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
    new Claim(ClaimTypes.Role, user.RoleId.ToString()),
    new Claim(ClaimTypes.Email, user.Email)
};


            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(360),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserInfoDTO GetUserInformation(string email)
        {
            var user = _dbContext.tbl_users.FirstOrDefault(u => u.Email == email);
            return user == null ? null : new UserInfoDTO
            {
                UserId = user.UserId,
                UserName = $"{user.FirstName} {user.LastName}",
                RoleId = user.RoleId,
                Email = user.Email
            };
        }

        public async Task<IActionResult> RequestPasswordReset(string email)
        {
            var user = _dbContext.tbl_users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return new BadRequestObjectResult(new { message = "User not found" });
            }

            var otp = new Random().Next(10000, 99999).ToString();
            var expiresAt = DateTime.UtcNow.AddMinutes(5);

            var passwordReset = new forgotpasswordreponse
            {
                UserId = user.UserId,
                Email = email,
                otp = otp,
                expires_at = expiresAt
            };

            _dbContext.password_reset.Add(passwordReset);
            await _dbContext.SaveChangesAsync();

            string subject = "Password Reset OTP - Angel Book House";
            string body = $"Dear {user.FirstName},\n\nYour OTP is: {otp}\nValid for 5 minutes.";

            bool emailSent = false;
            string errorMessage = "";

            try
            {
                emailSent = await _emailInterface.SendEmailAsync(
                    email, subject, body, "Angel Book House", "amruth2118reddy@gmail.com"
                );
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

                var log = new ErrorLog
                {
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    Module = "RequestPasswordReset",
                    ErrorType = "Email"
                };

                _dbContext.ErrorLogs.Add(log);
                await _dbContext.SaveChangesAsync();

                if (ex.InnerException != null)
                    errorMessage += " | Inner: " + ex.InnerException.Message;
            }

            if (!emailSent)
            {
                return new BadRequestObjectResult(new
                {
                    message = "OTP generated but email sending failed.",
                    error = errorMessage
                });
            }

            return new OkObjectResult(new { message = "OTP sent successfully", email });
        }



        public async Task<IActionResult> VerifyOtp(string email, string otp)
        {
            var otpRecord = _dbContext.password_reset
                .FirstOrDefault(o => o.Email == email && o.otp == otp && o.expires_at > DateTime.UtcNow);

            if (otpRecord == null)
            {
                return new BadRequestObjectResult(new { message = "Invalid or expired OTP" });
            }

            return new OkObjectResult(new { message = "OTP verified successfully", email });
        }

       public async Task<IActionResult> ResetPassword(string email, string password, string password_confirmation)
{
    if (string.IsNullOrWhiteSpace(email) || 
        string.IsNullOrWhiteSpace(password) || 
        string.IsNullOrWhiteSpace(password_confirmation))
    {
        return new BadRequestObjectResult(new { message = "Invalid request. All fields are required." });
    }

    if (password != password_confirmation)
    {
        return new BadRequestObjectResult(new { message = "Password and Confirm Password do not match." });
    }

    var user = _dbContext.tbl_users.FirstOrDefault(u => u.Email == email);
    if (user == null)
    {
        return new BadRequestObjectResult(new { message = "User not found." });
    }

    // Hash the new password
    user.Password = BCrypt.Net.BCrypt.HashPassword(password);
    _dbContext.tbl_users.Update(user);
    await _dbContext.SaveChangesAsync();

    // Remove OTP from the database
    var otpRecord = _dbContext.password_reset.Where(o => o.Email == email);
    _dbContext.password_reset.RemoveRange(otpRecord);
    await _dbContext.SaveChangesAsync();

    return new OkObjectResult(new { message = "Password reset successfully." });
}



    }
}