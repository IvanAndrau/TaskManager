using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerAPI.EF;

internal sealed class GroupMapper : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Group");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasColumnType("nvarchar(50)");
    }
}