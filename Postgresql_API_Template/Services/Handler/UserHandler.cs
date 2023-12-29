using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postgresql_API_Template.DataAccess;
using Postgresql_API_Template.Entities;
using Postgresql_API_Template.Entities.Dto;

namespace Postgresql_API_Template.Services.Handler
{
    public class UserHandler : IUserHandler
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        public UserHandler(ApplicationDbContext db, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUserService userRepository)
        {
            _db = db;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _userService = userRepository;
        }
        public async Task<string> DoLogin(UserLoginDto request)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                if (user == null || !_userService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)) { return "User not exist or password is incorrect"; }
                return await _userService.CreateToken(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> DoRegistration(UserRegistrationDto request)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                if (user != null) { return "User already exist"; }
                _userService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var result = new User()
                {
                    Email = request.Email,
                    Role = request.Role,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };
                await _db.Users.AddAsync(result);
                await _db.SaveChangesAsync();
                return await _userService.CreateToken(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
