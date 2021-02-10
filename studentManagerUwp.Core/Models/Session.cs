
using SQLite;
using System;
using System.Collections.Generic;

namespace studentManagerUwp
{
    class Session
    {
        
        public int Id { get; set; }
        public string date { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int courseId { get; set; }
        public int fieldId { get; set; }

        /*public Session()
        {
            db.createDatabase();
        }
        public bool Insert_Session(Session Session)
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    connection.Insert(Session);
                    SessionsAlbum.mBuiltInSessions = new Session().allSessions().ToArray();
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

        public List<Session> allSessions()
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    var data = connection.Table<Session>();
                    return data.ToList();
                }
                catch (Exception e)
                {
                    return new List<Session>();
                }
            }
        }

        internal bool editSession()
        {

            try
            {
                using (var connection = db.GetConnection())
                {
                    var data = connection.Table<Session>();
                    var data1 = (from values in data
                                 where values.Id == this.Id
                                 select values).FirstOrDefault();
                    data1.date = date;
                    data1.startTime = startTime;
                    data1.endTime = endTime;
                    data1.fieldId = fieldId;
                    data1.courseId = courseId;
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

        internal bool removeSession()
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
    }

    class SessionsAlbum
    {

        public static Session[] mBuiltInSessions = new Session().allSessions().ToArray();

        // Array of Sessions that make up the album:
        private Session[] mSessions;

        // Create an instance copy of the built-in Session list and
        public SessionsAlbum()
        {
            mSessions = mBuiltInSessions;
        }
        // Return the number of Sessions in the Session album:
        public int NumSessions
        {
            get { return mSessions.Length; }
        }
        // Indexer (read only) for accessing a Session:
        public Session this[int i]
        {
            get { return mSessions[i]; }
        }*/
    } 
}
