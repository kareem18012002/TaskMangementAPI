using Microsoft.EntityFrameworkCore;
using TaskMangementAPI.Models;

namespace TaskMangementAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TMDbContext _tMDbContext;

        public TaskRepository(TMDbContext tMDbContext)
        {
            _tMDbContext = tMDbContext;
        }

        public async Task AddAsync(TaskItem task)
        {
            await _tMDbContext.TaskItems.AddAsync(task);
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _tMDbContext.TaskItems
                   .Where(t => !t.IsDeleted)
                   .ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _tMDbContext.TaskItems
                   .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task SaveAsync()
        {
            await _tMDbContext.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(TaskItem task)
        {
            task.IsDeleted = true;                     // تعيين الحقل
            _tMDbContext.TaskItems.Update(task);       // تحديث الكائن
            await _tMDbContext.SaveChangesAsync();     // حفظ التغييرات
        }



       
            public async Task UpdateAsync(TaskItem task)
        {
            _tMDbContext.TaskItems.Update(task);
            await _tMDbContext.SaveChangesAsync();
            
        }
    
    }
}