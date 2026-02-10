using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskMangementAPI.Models;
using TaskMangementAPI.Repositories;

namespace TaskMangementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskItemController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return Ok(tasks);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                return NotFound();

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
                Status = dto.Status
            };

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
   
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _taskRepository.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Status = dto.Status;

            await _taskRepository.UpdateAsync(existing);
            await _taskRepository.SaveAsync();

            return NoContent();
        }





        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                return NotFound();

            await _taskRepository.SoftDeleteAsync(task);
            await _taskRepository.SaveAsync();

            return NoContent();
        }

    }
}
