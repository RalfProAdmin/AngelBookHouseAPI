using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkartAPI.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly EkartDBcontext _context;
        private readonly IEmailInterface _emailRepository;

        public UserRepository(EkartDBcontext context, IEmailInterface emailRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _emailRepository = emailRepository;
        }

        public async Task RegisterUserAsync(RegisterFKResponseModel user)
        {
            // Check if password and confirmation match
            if (user.password != user.password_confirmation)
            {
                throw new ArgumentException("Password and Confirm Password do not match.");
            }

            // Check if user already exists by email or mobile number
            bool userExists = await _context.tbl_users
                .AnyAsync(u => u.Email == user.email || u.MobileNumber == user.phone);

            if (userExists)
            {
                throw new ArgumentException("User already exists with the given email or mobile number.");
            }

            // Hash the password before storing
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.password);

            var newUser = new userModel
            {
                FirstName = user.name,
                LastName = "",
                Email = user.email,
                MobileNumber = user.phone,
                RoleId = 2,
                Password = hashedPassword,
                isActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Insert new user into the database
            await _context.tbl_users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            // Send a confirmation email
            string subject = "Welcome to Angel Book House!";
            string body = $"Dear {user.name},\n\n" +
              "Your account has been successfully created at Angel Book House.\n" +
              "You can now log in and explore our collection.\n\n" +
              "Click the link below to log in:\n" +
              "http://angelbookhouse.in/auth/login\n\n" +
              "Thank you for joining us!\n\n" +
              "Best Regards,\n" +
              "Angel Book House Team";

            bool emailSent = await _emailRepository.SendEmailAsync(
                user.email, subject, body, "Angel Book House", "amruth2118reddy@gmail.com"
            );

            if (!emailSent)
            {
                Console.WriteLine("User registered successfully, but email sending failed.");
            }
        }



        public async Task<userModel> GetUserByIdAsync(int userId)
        {
            return await _context.tbl_users.FindAsync(userId);
        }

        public async Task<IEnumerable<userModel>> GetAllUsersAsync()
        {
            return await _context.tbl_users.ToListAsync();
        }

        public async Task AddUserAsync(userModel user)
        {
            user.CreatedAt = DateTime.Now;
            await _context.tbl_users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(userModel user)
        {
            user.UpdatedAt = DateTime.Now;
            _context.tbl_users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.tbl_users.FindAsync(userId);
            if (user != null)
            {
                _context.tbl_users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserNameAsync(int userId, string firstName, string lastName)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC Sp_UpdateUserName @UserId = {0}, @FirstName = {1}, @LastName = {2}",
                userId, firstName, lastName);
        }

        public async Task UpdateUserEmailAsync(int userId, string email)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC Sp_UpdateUserEmail @UserId = {0}, @Email = {1}",
                userId, email);
        }

        public async Task UpdateUserPhoneNumberAsync(int userId, string mobileNumber)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC Sp_UpdateUserPhoneNumber @UserId = {0}, @MobileNumber = {1}",
                userId, mobileNumber);
        }
        public async Task<List<userModel>> GetRecentUsersFromSPAsync()
        {
            return await _context.tbl_users
                .FromSqlRaw("EXEC Sp_GetRecentUsers") // Ensure the procedure name is correct
                .ToListAsync();
        }

        public async Task<List<userModel>> GetDataForPageNProduct(int pageno, int pageSize)
        {
            return await _context.tbl_users
                .FromSqlRaw("EXEC getUsersByPageNoOrdersCount {0}, {1}", pageno, pageSize)
                .ToListAsync();
        }

        public async Task<int> GetUsersCountAsync()
        {
            var result = await _context.Set<userCount>()
                                       .FromSqlRaw("EXEC GetUsersRowCount")
                                       .ToListAsync();

            return result.FirstOrDefault()?.TotalRowCount ?? 0;
        }

        public async Task UpdateUserDetailsAsync(int id,UpdateUserDetails userDetails)
        {
             await _context.Database.ExecuteSqlRawAsync(
                "EXEC UpdateUserDetails @UserId={0}, @FirstName={1}, @MobileNumber={2},  @UpdatedAt={3}",
                id, userDetails.name, userDetails.phone, DateTime.UtcNow
            );

         
        }

        public async Task UpdatePasswordAsync(int id, string hashedPassword)
        {
            await _context.Database.ExecuteSqlRawAsync(
               "EXEC sp_UpdateUserPassword @UserId={0}, @NewPassword={1}",
               id, hashedPassword
           );
        }



    }
}
