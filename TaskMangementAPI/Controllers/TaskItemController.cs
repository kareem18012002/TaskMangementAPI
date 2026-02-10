using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskMangementAPI.Models;

namespace TaskMangementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly TMDbContext _tMDbContext;

        public TaskItemController(TMDbContext tMDbContext)
        {
            _tMDbContext = tMDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tasks = await _tMDbContext.taskItems
      .Where(t => !t.IsDeleted)
      .Select(t => new TaskDto
      {
          Id = t.Id,
          Title = t.Title,
          Description = t.Description,
          Status = t.Status
      })
      .ToListAsync();
            return Ok(tasks);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _tMDbContext.taskItems.Where(t=> t.Id == id && !t.IsDeleted).Select(t=> new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status
            }).FirstOrDefaultAsync();
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);

        }
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                Status = "Pending"
            };

            _tMDbContext.taskItems.Add(task);
            await _tMDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var task = await _tMDbContext.taskItems.FindAsync(id);

            if (task == null || task.IsDeleted)
                return NotFound();

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;

            await _tMDbContext.SaveChangesAsync();

            return NoContent();
        }



        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _tMDbContext.taskItems.FindAsync(id);
            if (task == null) return NotFound();
            task.IsDeleted = true;

            //_tMDbContext.taskItems.Remove(task);
            await _tMDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
    }
