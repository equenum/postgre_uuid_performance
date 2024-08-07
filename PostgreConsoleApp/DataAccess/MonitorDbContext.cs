using Microsoft.EntityFrameworkCore;

namespace PostgreConsoleApp.DataAccess;

internal class MonitorDbContext : DbContext
{
    public DbSet<TargetEntityV4> TargetsV4 { get; set; }
    public DbSet<TargetEntityV7> TargetsV7 { get; set; }

    public MonitorDbContext(DbContextOptions<MonitorDbContext> options)
        : base(options) 
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("monitor");
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TargetEntityConfigurationV4());
        modelBuilder.ApplyConfiguration(new TargetEntityConfigurationV7());
    }
}
