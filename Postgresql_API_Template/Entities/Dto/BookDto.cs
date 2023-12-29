namespace Postgresql_API_Template.Entities.Dto
{
    public class BookEditDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set;}
    }
    public class BookCreateDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
    }
}
