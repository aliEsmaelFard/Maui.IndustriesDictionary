using Maui.Dictionary.Model;
using SQLite;

namespace Maui.Dictionary.Repository
{
    public class MyRepository
    {
        private readonly SQLiteConnection _database;

        public static string DbPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "data.db");

        public MyRepository()
        {
            _database = new SQLiteConnection(DbPath);
            _database.CreateTable<WordsModel>();
        }

        public  List<WordsModel> GetList(string word)
        {
       //     int range2 = range + 20;
        //    var x =  _database.Query<WordsModel>("Select *  FROM words Limit "+range+", "+range2+"   ;");
            var x =  _database.Query<WordsModel>($"Select *  FROM words  Where English Like '{word}%'  Limit 20");
            return x;
        }

    }
}
