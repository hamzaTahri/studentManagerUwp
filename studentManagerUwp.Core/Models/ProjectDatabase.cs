
namespace studentManagerUwp.Core.Models
{
    public class ProjectDatabase
    {

        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        /************* Creat database and table *************/
        /*public bool createDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Absence.db")))
                {
                    connection.CreateTable<Person>();
                    connection.CreateTable<Professor>();
                    *//*connection.CreateTable<Classroom>();
                    connection.CreateTable<Field>();
                    connection.CreateTable<Student>();
                    connection.CreateTable<Course>();
                    connection.CreateTable<Session>();
                    connection.CreateTable<StudentSession>();*//*
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                //var messageDialog = new MessageDialog(ex.Message);
                //messageDialog.ShowAsync();
                return false;
            }
        }


        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(System.IO.Path.Combine(folder, "Absence.db"));
        }*/
    }
}
