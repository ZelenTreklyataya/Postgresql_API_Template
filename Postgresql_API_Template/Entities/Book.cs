using System.ComponentModel.DataAnnotations;

namespace Postgresql_API_Template.Entities
{
    public class Book
    {
        [Key]
        public int _id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public List<UserBook> UserBooks { get; set; }
    }
}
