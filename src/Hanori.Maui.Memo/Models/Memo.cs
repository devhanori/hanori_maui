using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hanori.Maui.Memo.Manager;

namespace Hanori.Maui.Memo.Models
{

    public class Memo : Manager.EFCoreManager<MemoDbContext>
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        private readonly MemoDbContext _dbContext;
        public Memo()
        {
            _dbContext = new MemoDbContext();
        }

        public void Save()
        {
            if (ReadOne(Name) != null)
            {
                var item = new MemoItem()
                {
                    Name = Name,
                    Date = Date,
                    Text = Text
                };

                _dbContext.Memos.Add(item);
                _dbContext.SaveChanges();
            }
        }

        public List<DbSet<MemoItem>> LoadAll()
        {
            return ReadAll()
                .Select(n => n.Memos)
                .ToList();
        }
    }

    [Table("Memo")]
    public class MemoItem
    {
        public MemoItem()
        {
            Name = Path.GetRandomFileName();
            Date = DateTime.Now;
            Text = string.Empty;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string Text { get; set; }

        public void Save()
        {
            
        }
    }
}

