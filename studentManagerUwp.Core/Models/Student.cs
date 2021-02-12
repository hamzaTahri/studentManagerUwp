
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;


using System.IO;
using System;
using SQLite.Net.Attributes;

namespace studentManagerUwp.Core.Models
{
    public class Student 
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Cin { get; set; }
        public string FullName { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public int FieldId { get; set; }


        public static bool Delete_Student(string cin)
        {

            var sqlCon = @"Data Source=C:\Users\ilkac\AppData\Local\Packages\C49BBD7C-8F7B-4A56-ABDC-753FC15ACC86_0g90rnz4tfct4\LocalState\studentManagerDatabase.db ;Version=3";
            using (SQLiteConnection connection = new SQLiteConnection())
            {
                connection.Open();
                string req = "delete from Students where cin='" + cin + "'";
                SQLiteCommand command = new SQLiteCommand(req, connection);
                var reader = command.ExecuteNonQuery();

                if (reader > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                connection.Close();
            }



        }

    }
}
