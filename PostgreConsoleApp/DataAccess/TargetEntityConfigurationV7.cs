using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgreConsoleApp.DataAccess;

internal class TargetEntityConfigurationV7 : IEntityTypeConfiguration<TargetEntityV7>
{
    public void Configure(EntityTypeBuilder<TargetEntityV7> builder)
    {
        builder.HasKey(m => m.Id);
        builder.ToTable("targets_v7");

        builder.Property(m => m.ChangeType).HasConversion<string>();
        builder.Property(m => m.SelectorType).HasConversion<string>();
    }
}
