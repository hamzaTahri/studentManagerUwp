using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Data.SQLite;
using System;
using System.Globalization;
using System.IO;

namespace studentManagerUwp.Core.Models
{
    public class DatabaseConnector
    {
        public static SQLiteConnection connection = new SQLiteConnection("Data Source=dbFile/studentManagerDatabase.db; Version=3");

        public static async Task LoadRecordsAsync(ObservableCollection<Professor> items)
        {
            
                connection.Open();
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
                reader.Close();
            }
            connection.Close();
        }
        public static async Task LoadRecordsAsync(ObservableCollection<Session> items)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=dbFile/studentManagerDatabase.db;Version=3"))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM Sessions", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var Id = reader.GetOrdinal("id");
                    var date = reader.GetOrdinal("date");
                    var startTime = reader.GetOrdinal("startTime");
                    var endTime = reader.GetOrdinal("endTime");
                    var fieldId = reader.GetOrdinal("fieldId");
                    var courseId = reader.GetOrdinal("courseId");

                    while (await reader.ReadAsync())
                    {
                        Session s = new Session();
                        s.Id = Convert.ToInt32(reader.GetValue(Id));
                        s.date = DateTime.ParseExact(reader.GetValue(date).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture); 
                        s.startTime = reader.GetValue(startTime).ToString();
                        s.endTime = reader.GetValue(endTime).ToString();
                        s.fieldId = Convert.ToInt32(reader.GetValue(fieldId).ToString());
                        s.courseId = Convert.ToInt32(reader.GetValue(courseId).ToString());

                        items.Add(s);
                    }
                    reader.Close();
                }
                connection.Close();
            }
        }
        public static async Task LoadRecordsAsync(ObservableCollection<Field> items)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=dbFile/studentManagerDatabase.db;Version=3"))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM Fields", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var Id = reader.GetOrdinal("id");
                    var name = reader.GetOrdinal("name");
                    var year = reader.GetOrdinal("year");
                    var description = reader.GetOrdinal("description");

                    while (await reader.ReadAsync())
                    {
                        Field f = new Field();
                        f.Id = Convert.ToInt32(reader.GetValue(Id));
                        f.name = reader.GetValue(name).ToString();
                        f.year = Convert.ToInt32(reader.GetValue(year).ToString());
                        f.description = reader.GetValue(description).ToString();

                        items.Add(f);
                    }
                    reader.Close();
                }
                connection.Close();
            }
        }

        public static async Task LoadRecordsAsync(ObservableCollection<Course> items)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=dbFile/studentManagerDatabase.db;Version=3"))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM Courses", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var Id = reader.GetOrdinal("id");
                    var name = reader.GetOrdinal("name");
                    var description = reader.GetOrdinal("description");
                    var profId = reader.GetOrdinal("profId");
                    var fieldId = reader.GetOrdinal("fieldId");

                    while (await reader.ReadAsync())
                    {
                        Course c = new Course();
                        c.Id = Convert.ToInt32(reader.GetValue(Id));
                        c.name = reader.GetValue(name).ToString();
                        c.description = reader.GetValue(description).ToString();
                        c.profId= Convert.ToInt32(reader.GetValue(profId).ToString());
                        c.fieldId = Convert.ToInt32(reader.GetValue(fieldId).ToString());

                        items.Add(c);
                    }
                    reader.Close();
                }
                connection.Close();
            }
        }
    }
}
