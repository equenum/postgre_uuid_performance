using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgreConsoleApp.DataAccess;

internal class TargetEntityConfigurationV4 : IEntityTypeConfiguration<TargetEntityV4>
{
    public void Configure(EntityTypeBuilder<TargetEntityV4> builder)
    {
        builder.HasKey(m => m.Id);
        builder.ToTable("targets_v4");

        builder.Property(m => m.ChangeType).HasConversion<string>();
        builder.Property(m => m.SelectorType).HasConversion<string>();
    }
}
