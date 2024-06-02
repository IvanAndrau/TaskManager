//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace TaskManagerAPI.EF;

public class TaskMgrContext : IdentityDbContext   //Add <IdentityUser>  ?
{
    public DbSet<Task> Tasks { get; set; }

    public DbSet<Group> Groups { get; set; }


    public TaskMgrContext(DbContextOptions<TaskMgrContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
