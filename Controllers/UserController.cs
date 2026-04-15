using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Repository.Implementation;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

namespace EkartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        [HttpGet("UserDetailsFK")]
        [Authorize]
        public async Task<IActionResult> GetUserByIdFK()
        {
            var userIdClaim = User.FindFirst("userId");
           
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }
            var existinguser = await _userRepository.GetUserByIdAsync(userId);
            if (existinguser == null)
            {
                return NotFound();
            }

            var response = new UserDetailsFKResponse
            {
                user_id = existinguser.UserId,
                name = existinguser.FirstName,
                email = existinguser.Email,
                phone = existinguser.MobileNumber,

            };

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterFKResponseModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid registration data.");
            }

            try
            {
                await _userRepository.RegisterUserAsync(user);
                return Ok(new { message = "User registered successfully." });
            }
            catch (ArgumentException ex)
            {
              
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { error = "An error occurred during registration.", details = ex.Message });
            }
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var response = users.Select(users => new userModel
            {
                UserId = users.UserId,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Email = users.Email,
                RoleId = users.RoleId,
                MobileNumber = users.MobileNumber,
                isActive = users.isActive,
                CreatedAt= DateTime.UtcNow,
                UpdatedAt= DateTime.UtcNow,
            }).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Createuser(PostUserDTO Request)
        {
            var users = new userModel
            {
                
                FirstName = Request.FirstName,
                LastName = Request.LastName,
                Email = Request.Email,
                MobileNumber = Request.MobileNumber,
                isActive = true,
                Password = Request.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                RoleId = 2,
            };

            await _userRepository.AddUserAsync(users);

            var response = new userModel
            {
                UserId = users.UserId,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Email = users.Email,
                MobileNumber = users.MobileNumber,
                isActive = users.isActive,
                Password = users.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                RoleId = users.RoleId,
            };

            return Ok(response);

        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
         {
            var existinguser = await _userRepository.GetUserByIdAsync(id);
            if (existinguser == null)
            {
                return NotFound();
            }

            var response = new userModel
            {
                UserId = existinguser.UserId,
                FirstName = existinguser.FirstName,
                LastName = existinguser.LastName,
                Email = existinguser.Email,
                MobileNumber = existinguser.MobileNumber,
                isActive = existinguser.isActive,
                CreatedAt = existinguser.CreatedAt,
                UpdatedAt = existinguser.UpdatedAt,
            };

            return Ok(response);
        }

        [HttpPut("UpdateName/{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserName(UpdateNameRequest request)
        {
            if (request.UserId != request.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            await _userRepository.UpdateUserNameAsync(request.UserId, request.FirstName, request.LastName);
            return NoContent();
        }

        [HttpPut("UpdateEmail/{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserEmail(UpdateEmailRequest request)
        {
            if (request.UserId != request.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            await _userRepository.UpdateUserEmailAsync(request.UserId, request.Email);
            return NoContent();
        }

        [HttpPut("UpdatePhone/{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserPhoneNumber(UpdatePhoneRequest request)
        {
            if (request.UserId != request.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            await _userRepository.UpdateUserPhoneNumberAsync(request.UserId, request.MobileNumber);
            return NoContent();
        }

        [HttpGet("PAGEn-Data")]
        [Authorize]
        public async Task<IActionResult> GetUserDataForPageN(int pageno, int pageSize)
        {
            try
            {
                // Get paginated user data
                var userData = await _userRepository.GetDataForPageNProduct(pageno, pageSize);

                // Get total user count
                var userCount = await _userRepository.GetUsersCountAsync();

                // Return the result as a combined object
                return Ok(new { UserData = userData, UserCount = userCount });
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error response with the exception message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("updateuserFK")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDetails userDetails)
        {


            var userIdClaim = User.FindFirst("userId");
           

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }
            if (userId != userId)
            {
                return BadRequest("User ID mismatch.");
            }

            await _userRepository.UpdateUserDetailsAsync(userId, userDetails);
            return Ok(new { message = "User updated successfully", userId = userDetails.id });
        }

        [HttpPut("updatepasswordFK")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword( [FromBody] UpdatePasswordFK userPassword)
        {
            var userIdClaim = User.FindFirst("userId");
            

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid User ID format" });
            }

          

            if (string.IsNullOrWhiteSpace(userPassword.current_password) ||
                string.IsNullOrWhiteSpace(userPassword.password) ||
                string.IsNullOrWhiteSpace(userPassword.confirm_password))
            {
                return BadRequest(new { message = "Passwords cannot be null or empty." });
            }

            if (userPassword.password != userPassword.confirm_password)
            {
                return BadRequest(new { message = "New password and confirm password do not match." });
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            // 🔹 Compare hashed password using BCrypt (ensure non-null)
            if (!BCrypt.Net.BCrypt.Verify(userPassword.current_password ?? "", user.Password ?? ""))
            {
                return BadRequest(new { message = "Current password is incorrect." });
            }
            if (BCrypt.Net.BCrypt.Verify(userPassword.password ?? "", user.Password ?? ""))
            {
                return BadRequest(new { message = "Current password and new Password should not be same." });
            }


            // 🔹 Hash the new password (ensure non-null)
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userPassword.password ?? "");

            await _userRepository.UpdatePasswordAsync(userId, hashedPassword);

            return Ok(new { message = "Password updated successfully", userId = userId });
        }



    }
}
