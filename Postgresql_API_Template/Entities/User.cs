using Postgresql_API_Template.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Postgresql_API_Template.Entities
{
    public class User
    {
        [Key]
        public int _id { get; set; }
        public string? Name { get; set; }
        public Enums.Role Role { get; set; } = Enums.Role.User;
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<UserBook> UserBooks { get; set; }
    }
    public class UserBook
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
