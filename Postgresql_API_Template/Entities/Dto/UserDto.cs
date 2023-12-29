using Postgresql_API_Template.Helpers;

namespace Postgresql_API_Template.Entities.Dto
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserRegistrationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Enums.Role Role { get; set; } = Enums.Role.User;
    }
}
