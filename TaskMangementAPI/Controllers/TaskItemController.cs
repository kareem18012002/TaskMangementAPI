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
            var tasks = await _tMDbContext.taskItems.ToListAsync();
            return Ok(tasks);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _tMDbContext.taskItems.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);

        }
        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskItem taskItem)
        {
            _tMDbContext.taskItems.Add(taskItem);
            await _tMDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = taskItem.Id }, taskItem);
        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return BadRequest();
            }
            _tMDbContext.Entry(taskItem).State = EntityState.Modified;
            try
            {
                await _tMDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (id != taskItem.Id)
                {
                    return BadRequest();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _tMDbContext.taskItems.FindAsync(id);
            if (task == null) return NotFound();

            _tMDbContext.taskItems.Remove(task);
            await _tMDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
    }
