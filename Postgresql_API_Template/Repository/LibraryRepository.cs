using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postgresql_API_Template.DataAccess;
using Postgresql_API_Template.Entities;
using Postgresql_API_Template.Entities.Dto;
using System.Security.Claims;

namespace Postgresql_API_Template.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LibraryRepository(IConfiguration configuration, ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Add(int id)
        {
            try
            {
                var username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == username);
                var userbookExist = await _db.UserBooks.FirstOrDefaultAsync(x => x.BookId == id && x.UserId == user._id);
                if (userbookExist != null) { return; }
                var book = await _db.Books.FirstOrDefaultAsync(x => x._id == id);
                var userbook = new UserBook { User = user, Book = book };
                await _db.UserBooks.AddAsync(userbook);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(BookCreateDto item)
        {
            try
            {
                var book = new Book()
                {
                    Author = item.Author,
                    Description = item.Description,
                    Genre = item.Genre,
                    Title = item.Title
                };
                await _db.Books.AddAsync(book);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var book = await _db.Books.FirstOrDefaultAsync(x => x._id == id);
                if (book == null) { return; }
                _db.Books.Remove(book);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Book>> Get()
        {
            try
            {
                var books = await _db.Books.AsQueryable().ToListAsync();
                return books;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Book> GetOne(int id)
        {
            try
            {
                var book = await _db.Books.FirstOrDefaultAsync(x => x._id == id);
                return book;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Book>> GetMe()
        {
            try
            {
                var books = _db.UserBooks.Where(ub => ub.User.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name)).Select(ub => ub.Book).ToList();
                return books;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task Remove(int id)
        {
            try
            {
                var username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == username);
                var userbookExist = await _db.UserBooks.FirstOrDefaultAsync(x => x.BookId == id);
                if (userbookExist == null) { return; }
                _db.UserBooks.Remove(userbookExist);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
