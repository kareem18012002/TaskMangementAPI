using System.Collections.Generic;
using TaskMangementAPI.Models;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem> GetByIdAsync(int id);
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task SoftDeleteAsync(TaskItem task);
    Task SaveAsync();
}
