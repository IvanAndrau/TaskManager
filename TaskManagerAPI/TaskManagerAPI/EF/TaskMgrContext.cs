using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace TaskManagerAPI.EF;

public class TaskMgrContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }

    public DbSet<Group> Groups { get; set; }


    public TaskMgrContext(DbContextOptions<TaskMgrContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

}
