using Postgresql_API_Template.Entities;

namespace Postgresql_API_Template.Services
{
    public interface IUserService
    {
        Task<string> CreateToken(User user);
        Task<string> RefreshToken(string token);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
