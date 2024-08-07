using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        const int targetPerBatchCount = 1000;

        var v4Ticks = new List<long>(); 
        var v7Ticks = new List<long>(); 

        for (int i = 0; i <= batchCount ; i++)
        {
            // insert uuid v4 entries 

            var swV4 = Stopwatch.StartNew();

            for (int j = 1; j <= targetPerBatchCount; j++)
            {
                var target = CreateTargetV4(Guid.NewGuid());

                _context.TargetsV4.Add(target);
                await _context.SaveChangesAsync();
            }
            
            swV4.Stop();

            // here we disregard the results of the first 'warmup' batch
            if (i != 0) 
            {
                v4Ticks.Add(swV4.ElapsedMilliseconds);
                Console.WriteLine($"UUID v4 batch {i}/{batchCount} completed.");
            }

            // insert uuid v7 entries 

            var swV7 = Stopwatch.StartNew();

            for (int j = 1; j <= targetPerBatchCount; j++)
            {
                var target = CreateTargetV7(Uuid.NewDatabaseFriendly(Database.PostgreSql));

                _context.TargetsV7.Add(target);
                await _context.SaveChangesAsync();
            }

            swV7.Stop();

            if (i != 0) 
            {
                v7Ticks.Add(swV7.ElapsedMilliseconds);
                Console.WriteLine($"UUID v7 batch {i}/{batchCount} completed.");
            }
        }

        Console.WriteLine($"UUID v4: {string.Join(", ", v4Ticks)}");
        Console.WriteLine(v4Ticks.Average());

        Console.WriteLine($"UUID v7: {string.Join(", ", v7Ticks)}");
        Console.WriteLine(v7Ticks.Average());
    }

    private TargetEntityV4 CreateTargetV4(Guid id) => new TargetEntityV4() 
    {
        Id = id,
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

    private TargetEntityV7 CreateTargetV7(Guid id) => new TargetEntityV7() 
    {
        Id = id,
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
