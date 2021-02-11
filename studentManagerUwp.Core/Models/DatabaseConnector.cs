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

        public static async Task LoadRecordsAsyncForStudent(ObservableCollection<Student> items)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=dbFile/studentManagerDatabase.db;Version=3"))
            {
                await connection.OpenAsync();
                SQLiteCommand command = new SQLiteCommand("SELECT *  FROM Students", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var Id = reader.GetOrdinal("id");
                    var fullName = reader.GetOrdinal("fullName");
                    var email = reader.GetOrdinal("email");
                    var cin = reader.GetOrdinal("cin");
                    var tel = reader.GetOrdinal("tel");
                    var fieldid = reader.GetOrdinal("fieldId");


                    while (await reader.ReadAsync())
                    {
                        Student s = new Student();
                        s.Id= Convert.ToInt32(reader.GetValue(Id));
                        s.Cin= reader.GetValue(cin).ToString();
                        s.Email= reader.GetValue(email).ToString();
                        s.FullName= reader.GetValue(fullName).ToString();
                        s.Tel = reader.GetValue(tel).ToString();
                        s.FieldId= Convert.ToInt32(reader.GetValue(fieldid));
                        items.Add(s);
                    }
                }
            }


        }


        public static async Task LoadRecordsAsyncForCourse(ObservableCollection<Course> items)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=dbFile/studentManagerDatabase.db;Version=3"))
            {
                await connection.OpenAsync();
                SQLiteCommand command = new SQLiteCommand("SELECT *  FROM Courses", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var Id = reader.GetOrdinal("id");
                    var name = reader.GetOrdinal("name");
                    var description = reader.GetOrdinal("description");
                    var fieldid = reader.GetOrdinal("fieldId");
                    var profId = reader.GetOrdinal("profId");


                    while (await reader.ReadAsync())
                    {
                        Course c = new Course();
                        c.Id = Convert.ToInt32(reader.GetValue(Id));
                        c.name= reader.GetValue(name).ToString();
                        c.description = reader.GetValue(description).ToString();
                        c.fieldId= Convert.ToInt32(reader.GetValue(fieldid));
                        c.profId = Convert.ToInt32(reader.GetValue(profId));
                        items.Add(c);
                    }
                }
            }


        }



        public static async Task LoadRecordsAsyncForField(ObservableCollection<Field> items)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=dbFile/studentManagerDatabase.db;Version=3"))
            {
                await connection.OpenAsync();
                SQLiteCommand command = new SQLiteCommand("SELECT *  FROM Fields ", connection);

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
                        f.description = reader.GetValue(description).ToString();
                        f.year = Convert.ToInt32(reader.GetValue(year));

                        items.Add(f);
                    }
                }
            }


        }


    }
}
