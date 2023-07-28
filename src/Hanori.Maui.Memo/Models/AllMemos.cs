using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace Hanori.Maui.Memo.Models
{
    public class AllMemos : Manager.EFCoreManager<MemoDbContext>
    {

        public ObservableCollection<MemoDbContext> Memos { get; private set; }

        public void UpdateMemos()
        {
            Memos.Clear();
            Memos = new ObservableCollection<MemoDbContext>(ReadAll());
        }

        public AllMemos()
        {

        }
    }


    public class MemoDbContext : DbContext
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
