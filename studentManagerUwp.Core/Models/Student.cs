
using SQLite;
using System;
using System.Collections.Generic;

namespace studentManagerUwp
{
    public class Student 
    {
        public string cin { get; set; }
        public string Firstname { get; set; }
        public string Tel { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int fieldId { get; set; }




/*
        ProjectDatabase db = new ProjectDatabase();

        public Student()
        {
            db.createDatabase();
        }

        public bool Insert_Student(Student Student)
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    connection.Insert(Student);
                    StudentsAlbum.mBuiltInStudents = new Student().allStudents().ToArray();
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


        public bool Is_Exist_Student(string name)
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    var data = connection.Table<Student>();
                    var data1 = data.Where(x => x.name == name).FirstOrDefault();
                    if (data1 == null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                messageDialog.ShowAsync();
                return false;
            }
        }

        internal bool remove()
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    var data = connection.Table<Student>();
                    var data1 = data.Where(x => x.Id == Id).FirstOrDefault();
                    connection.Delete(data1);
                }

                return true;
            }
            catch (SQLiteException ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                messageDialog.ShowAsync();
                return false;
            }
        }

        internal Student[] studentsFromField(int fieldId)
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    var data = connection.Table<Student>();
                    return data.Where(x => x.fieldId == fieldId).ToArray();
                }
            }
            catch (SQLiteException ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                messageDialog.ShowAsync();
                return null;
            }
        }

        public List<Student> allStudents()
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    var data = connection.Table<Student>();
                    return data.ToList();
                }
                catch (Exception e)
                {
                    return new List<Student>();
                }
            }
        }
        public List<string> allStudentsNames()
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    var data = connection.Table<Student>();
                    List<Student> list = data.ToList();
                    List<string> listToReturn = new List<string>();
                    foreach (Student item in list)
                    {
                        listToReturn.Add(item.name);
                    }
                    return listToReturn;
                }
                catch (Exception e)
                {
                    return new List<string>();
                }
            }
        }
    }

    class StudentsAlbum
    {

        public static Student[] mBuiltInStudents = new Student().allStudents().ToArray();

        // Array of Students that make up the album:
        private Student[] mStudents;

        // Create an instance copy of the built-in Student list and
        public StudentsAlbum()
        {
            mStudents = mBuiltInStudents;
        }
        // Return the number of Students in the Student album:
        public int NumStudents
        {
            get { return mStudents.Length; }
        }
        // Indexer (read only) for accessing a Student:
        public Student this[int i]
        {
            get { return mStudents[i]; }
        }*/
    }
}
