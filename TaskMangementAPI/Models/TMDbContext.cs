using Microsoft.EntityFrameworkCore;

namespace TaskMangementAPI.Models;

internal class TMDbContext : DbContext
{
    public TMDbContext(DbContextOptions<TMDbContext> options) : base(options)
    {
      
    }
    public DbSet<TaskItem> taskItems  { get; set; }
}