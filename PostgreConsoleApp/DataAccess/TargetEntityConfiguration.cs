using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgreConsoleApp.DataAccess;

internal class TargetEntityConfiguration : IEntityTypeConfiguration<TargetEntity>
{
    public void Configure(EntityTypeBuilder<TargetEntity> builder)
    {
        builder.HasKey(m => m.Id);
        builder.ToTable("targets");

        builder.Property(m => m.ChangeType).HasConversion<string>();
        builder.Property(m => m.SelectorType).HasConversion<string>();
    }
}
