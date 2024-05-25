using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerAPI.EF;

internal sealed class TaskMapper : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("Task");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description).HasColumnType("nvarchar(100)");
        builder.Property(x => x.Name).HasColumnType("nvarchar(50)");

        builder.HasOne(x => x.Group).WithMany(x => x.Tasks).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.Restrict);
    }
}

