using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hanori.Maui.Memo.Models
{
    public class AllMemos : DbContext
    {
        DbSet<Memo> Memos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "";
            var serverVersion = new MariaDbServerVersion(new Version(10, 3, 2));
            optionsBuilder.UseMySql(connectionString, serverVersion)
                                .LogTo(Console.WriteLine, LogLevel.Information)
                                .EnableSensitiveDataLogging()
                                .EnableDetailedErrors();
        }
    }
}
