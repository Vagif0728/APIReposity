using API.Services.Implementations;
using API.Services.Interfaces;


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page, int take)
        {
            return Ok(await _tagService.GetAllByOrderAsync(page, take, c => c.Name));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(await _tagService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTagDto createTagDto)
        {
            await _tagService.CreateAsync(createTagDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateTagDto createTagDto)
        {
            if (id <= 0) return BadRequest();
            await _tagService.UpdateAsync(id, createTagDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            await _tagService.DeleteAsync(id);
            return NoContent();
        }
    }
}
