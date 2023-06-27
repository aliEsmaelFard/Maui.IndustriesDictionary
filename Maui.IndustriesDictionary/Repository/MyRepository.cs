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

        public  List<WordsModel> GetList(string word, string lang)
        {
            var x =  _database.Query<WordsModel>($"Select *  FROM words  Where {lang} Like '{word}%'  Limit 20");
            return x;
        }

    }
}
