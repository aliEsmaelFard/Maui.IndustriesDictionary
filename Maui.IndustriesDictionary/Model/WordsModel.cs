using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Dictionary.Model
{
    [Table("word")]
    public class WordsModel
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string Deutsch { get; set; }
        public string English { get; set; }
        public string Farsi { get; set; }
    }
}
