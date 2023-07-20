using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanori.Maui.Memo.Models
{
    public class Memo
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
