using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanori.Maui.Memo.Models
{
    
    [Table("Memo")]
    public class Memo
    {
        public Memo() 
        { 
            Date = DateTime.Now;
            Text = string.Empty;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public DateTime Date { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string Text { get; set; }
    }
}
