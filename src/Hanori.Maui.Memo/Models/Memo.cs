using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hanori.Maui.Memo.Models
{
    [Table("Memo")]
    public class Memo
    {
        public Memo()
        {
            _dbContext = new MemoDbContext();
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

        private readonly MemoDbContext _dbContext;

        public Memo(string Name)
        {
            _dbContext = new MemoDbContext();
            if(_dbContext.Memos.Find(Name) != null)
            {
                this.Name = Name;
                this.Date = _dbContext.Memos.Find(Name).Date;
                this.Text = _dbContext.Memos.Find(Name).Text;
            }
            else
            {
                this.Name = Name;
                this.Date = DateTime.Now;
                this.Text = "";
            }
        }

        public void Save()
        {
            if (_dbContext.Memos.Find(Name) != null)
            {
                _dbContext.Memos.Find(Name).Date = this.Date;
                _dbContext.Memos.Find(Name).Name = this.Name;
                _dbContext.Memos.Find(Name).Text = this.Text;
                _dbContext.SaveChanges();
            }
            else
            {
                var item = new Memo()
                {
                    Name = Name,
                    Date = Date,
                    Text = Text
                };

                _dbContext.Memos.Add(item);
                _dbContext.SaveChanges();
            }
        }

        public void Delete()
        {
            if (_dbContext.Memos.Find(Name) != null)
            {
                var entity = _dbContext.Memos.Find(Name);
                _dbContext.Remove(entity);
            }
            _dbContext.SaveChanges();
        }

        public static List<Memo> ReadAll()
        {
            var context = new MemoDbContext();
            return context.Memos.ToList();
        }

        public static Memo ReadOne(string Name)
        {
            var context = new MemoDbContext();
            return context.Memos.Find(Name);
        }
    }
}

