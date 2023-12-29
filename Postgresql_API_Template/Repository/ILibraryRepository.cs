using Postgresql_API_Template.Entities;
using Postgresql_API_Template.Entities.Dto;

namespace Postgresql_API_Template.Repository
{
    public interface ILibraryRepository
    {
        Task<List<Book>> Get();
        Task<Book> GetOne(int id);
        Task<List<Book>> GetMe();
        Task Create(BookCreateDto item);
        Task Add(int id);
        Task Remove(int id);
        Task Delete(int id);
    }
}
