
using SQLite;
using System;
using System.Collections.Generic;

namespace studentManagerUwp.Core.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int profId { get; set; }
        public int fieldId { get; set; }


        /*public Course()
        {
            db.createDatabase();
        }

        public bool Insert_Course(Course Course)
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    connection.Insert(Course);
                    CoursesAlbum.mBuiltInCourses = new Course().allCourses().ToArray();
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

        public Course courseById(int courseId)
        {
            using (var connection = db.GetConnection())
            {
                var data = connection.Table<Course>();
                return data.Where(x => x.Id == courseId).FirstOrDefault();
            }
        }

        public bool Is_Exist_Course(string name)
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    var data = connection.Table<Course>();
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
                    var data = connection.Table<Course>();
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

        public List<string> allCoursesNames()
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    var data = connection.Table<Course>();
                    List<Course> list = data.ToList();
                    List<string> listToReturn = new List<string>();
                    foreach (Course item in list)
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

        public List<Course> allCourses()
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    var data = connection.Table<Course>();
                    return data.ToList();
                }
                catch (Exception e)
                {
                    return new List<Course>();
                }
            }
        }
        internal bool editCourse()
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    var data = connection.Table<Course>();
                    var data1 = (from values in data
                                 where values.Id == this.Id
                                 select values).FirstOrDefault();
                    data1.name = name;
                    data1.description = description;
                    data1.fieldId = fieldId;
                    data1.profId = profId;
                    connection.Update(data1);

                    return true;
                }
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                messageDialog.ShowAsync();
                return false;
            }
        }
    }

    class CoursesAlbum
    {

        public static Course[] mBuiltInCourses = new Course().allCourses().ToArray();

        // Array of Courses that make up the album:
        private Course[] mCourses;

        // Create an instance copy of the built-in Course list and
        public CoursesAlbum()
        {
            mCourses = mBuiltInCourses;
        }
        // Return the number of Courses in the Course album:
        public int NumCourses
        {
            get { return mCourses.Length; }
        }
        // Indexer (read only) for accessing a Course:
        public Course this[int i]
        {
            get { return mCourses[i]; }
        }*/
    }
}
