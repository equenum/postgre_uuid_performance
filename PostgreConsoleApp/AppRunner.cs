using System;
using System.Threading.Tasks;
using PostgreConsoleApp.DataAccess;
using UUIDNext;

namespace PostgreConsoleApp;

internal class AppRunner
{
    private const string DisplayName = "DisplayName";
    private const string Description = "Description";
    private const string Url = "https://test.test";
    private const string CronSchedule = "CronSchedule";
    private const string HtmlTag = "div";
    private const string SelectorValue = "SelectorValue";
    private const string ExpectedValue = "ExpectedValue";

    private readonly DateTime _createdAt = DateTime.UtcNow;
    private readonly DateTime _updatedAt = DateTime.UtcNow;
    private readonly Guid _resourceId = Guid.NewGuid();
    private readonly MonitorDbContext _context;

    public AppRunner(MonitorDbContext context)
    {
        _context = context;
    }
    
    internal async Task RunAsync(string[] args)
    {
        const int batchCount = 10;
        const int targetPerBatchCount = 10;

        // insert 100.000 records with uuid v4

        for (int i = 1; i <= batchCount; i++)
        {
            for (int j = 1; j <= targetPerBatchCount; j++)
            {
                var target = CreateTarget(Guid.NewGuid());

                _context.Targets.Add(target);
                await _context.SaveChangesAsync();
            }
            
            Console.WriteLine($"UUID v4 batch {i}/{batchCount} completed.");
        }

        // insert 100.000 records with uuid v7

        for (int i = 1; i <= batchCount; i++)
        {
            for (int j = 1; j <= targetPerBatchCount; j++)
            {
                var target = CreateTarget(Uuid.NewDatabaseFriendly(Database.PostgreSql));

                _context.Targets.Add(target);
                await _context.SaveChangesAsync();
            }
            
            Console.WriteLine($"UUID v7 batch {i}/{batchCount} completed.");
        }
    }

    private TargetEntity CreateTarget(Guid id) => new TargetEntity() 
    {
        Id = Uuid.NewDatabaseFriendly(Database.PostgreSql),
        DisplayName = DisplayName,
        Description = Description,
        Url = Url,
        CronSchedule = CronSchedule,
        ChangeType = ChangeType.Value,
        HtmlTag = HtmlTag,
        SelectorType = SelectorType.Class,
        SelectorValue = SelectorValue,
        ExpectedValue = ExpectedValue,
        CreatedAt = _createdAt,
        UpdatedAt = _updatedAt,
        ResourceId = _resourceId
    };
}
