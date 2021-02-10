using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Data.SQLite;
using System;

namespace studentManagerUwp.Core.Models
{
    public class DatabaseConnector
    {

        public static async Task LoadRecordsAsync(ObservableCollection<Professor> items)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=dbFile/studentManagerDatabase.db;Version=3"))
            {
                await connection.OpenAsync();
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM Professors", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var Id = reader.GetOrdinal("id");
                    var fullName = reader.GetOrdinal("fullName");
                    var email = reader.GetOrdinal("email");
                    var password = reader.GetOrdinal("password");

                    while (await reader.ReadAsync())
                    {
                        Professor p = new Professor();
                        p.Id = Convert.ToInt32(reader.GetValue(Id) );
                        p.fullName = reader.GetValue(fullName).ToString();
                        p.email = reader.GetValue(email).ToString();
                        p.password = reader.GetValue(password).ToString();
                        items.Add(p);
                    }
                }
            }


        }
    }
}
