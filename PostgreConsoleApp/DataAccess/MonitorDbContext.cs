using Microsoft.EntityFrameworkCore;

namespace PostgreConsoleApp.DataAccess;

internal class MonitorDbContext : DbContext
{
    public DbSet<TargetEntity> Targets { get; set; }

    public MonitorDbContext(DbContextOptions<MonitorDbContext> options)
        : base(options) 
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("monitor");
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TargetEntityConfiguration());
    }
}
