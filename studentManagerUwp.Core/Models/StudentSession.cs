
using SQLite;
using System;
using System.Collections.Generic;

namespace studentManagerUwp.Core.Models
{
    public class StudentSession
    {
        public int Id { get; set; }
        public int sessionId { get; set; }
        public int studentId { get; set; }

      /*  public StudentSession()
        {
            db.createDatabase();
        }
        public bool Insert_StudentSession(StudentSession StudentSession)
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    connection.Insert(StudentSession);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                messageDialog.ShowAsync();
                return false;
            }
        }

        public List<StudentSession> allStudentSessions()
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    var data = connection.Table<StudentSession>();
                    return data.ToList();
                }
                catch (Exception e)
                {
                    return new List<StudentSession>();
                }
            }
        }*/

    }
}
