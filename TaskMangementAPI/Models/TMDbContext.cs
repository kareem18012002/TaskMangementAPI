using Microsoft.EntityFrameworkCore;

namespace TaskMangementAPI.Models;

public class TMDbContext : DbContext
{
    public TMDbContext(DbContextOptions<TMDbContext> options) : base(options)
    {
      
    }
    public DbSet<TaskItem> TaskItems  { get; set; }
}