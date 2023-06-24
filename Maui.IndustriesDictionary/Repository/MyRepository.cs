using Maui.Dictionary.Model;
using SQLite;

namespace Maui.Dictionary.Repository
{
    public class MyRepository
    {
        private readonly SQLiteConnection _database;

        public static string DbPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "data.sqlite");

        public MyRepository()
        {
            _database = new SQLiteConnection(DbPath);
            _database.CreateTable<WordsModel>();
        }

        public  List<WordsModel> GetList(string word)
        {
            var x =  _database.Query<WordsModel>($"Select *  FROM words  Where English Like '{word}%'  Limit 20");
            return x;
        }

    }
}
