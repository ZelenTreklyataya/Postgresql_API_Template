using Postgresql_API_Template.Entities.Dto;

namespace Postgresql_API_Template.Services.Handler
{
    public interface IUserHandler
    {
        Task<string> DoLogin(UserLoginDto request);
        Task<string> DoRegistration(UserRegistrationDto request);
    }
}
