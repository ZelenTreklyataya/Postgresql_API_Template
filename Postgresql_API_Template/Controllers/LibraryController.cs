
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postgresql_API_Template.Entities;
using Postgresql_API_Template.Entities.Dto;
using Postgresql_API_Template.Repository;

namespace Postgresql_API_Template.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class LibraryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILibraryRepository _libraryRepository;
        public LibraryController(IConfiguration configuration, ILibraryRepository libraryRepository)
        {
            _configuration = configuration;
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            try
            {
                var result = await _libraryRepository.Get();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetOne(int id)
        {
            try
            {
                var result = await _libraryRepository.GetOne(id);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("me")]
        public async Task<ActionResult<List<Book>>> GetMe()
        {
            try
            {
                var result = await _libraryRepository.GetMe();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost, Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> Create(BookCreateDto book)
        {
            try
            {
                await _libraryRepository.Create(book);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete, Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _libraryRepository.Delete(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("add")]
        public async Task<ActionResult> Add(int id)
        {
            try
            {
                await _libraryRepository.Add(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("remove")]
        public async Task<ActionResult> Remove(int id)
        {
            try
            {
                await _libraryRepository.Remove(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
