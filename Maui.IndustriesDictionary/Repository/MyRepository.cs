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

        public List<WordsModel> GetList(string word, string lang, string advanceSearch)
        {
            string query;
            if(advanceSearch == "Start")
                 query = $"Select *  FROM word  Where {lang} Like '{word}%'  Limit 20";
            else
                query = $"Select *  FROM word  Where {lang} Like '%{word}%'  Limit 20";

            //  string query2 = $"Select *  FROM word";
            List<WordsModel> x = _database.Query<WordsModel>(query);
            return x;
        }

    }
}
