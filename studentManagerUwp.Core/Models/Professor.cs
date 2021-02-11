using System;
using System.Collections.Generic;

namespace studentManagerUwp.Core.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
       
/*
        public List<Professor> allProfs()
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    var data = connection.Table<Professor>();
                    return data.ToList();
                }
                catch (Exception e)
                {
                    return new List<Professor>();
                }
            }
        }

        public bool Insert_Prof()
        {
            try
            {

                using (var connection = db.GetConnection())
                {
                    connection.Insert(this);
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

        internal bool remove()
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    var data = connection.Table<Professor>();
                    var data1 = data.Where(x => x.Id == Id).FirstOrDefault();
                    connection.Delete(data1);
                }

                return true;
            }
            catch (SQLiteException ex)
            {
                //var messageDialog = new MessageDialog(ex.Message);
                //messageDialog.ShowAsync();
                return false;
            }
        }

        public bool Is_Exist_Prof(string email)
        {
            try
            {

                using (var connection = db.GetConnection())
                {
                    var data = connection.Table<Professor>();
                    var data1 = data.Where(x => x.email == email).FirstOrDefault();
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
                //var messageDialog = new MessageDialog(ex.Message);
                //messageDialog.ShowAsync();
                return false;
            }
        }

        public bool Login_Prof(string user, string pwd)
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    var data = connection.Table<Professor>();
                    var data1 = data.Where(x => x.username == user && x.password == pwd).FirstOrDefault();
                    if (data1 != null)
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
                //var messageDialog = new MessageDialog(ex.Message);
                //messageDialog.ShowAsync();
                return false;
            }
        }



        internal bool editProf()
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    var data = connection.Table<Professor>();
                    var data1 = (from values in data
                                 where values.Id == this.Id
                                 select values).FirstOrDefault();
                    data1.username = username;
                    data1.email = email;
                    data1.password = password;
                    connection.Update(data1);

                    return true;
                }
            }
            catch (Exception ex)
            {
                //var messageDialog = new MessageDialog(ex.Message);
                //messageDialog.ShowAsync();
                return false;
            }
        }

        public List<string> allProfsNames()
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    var data = connection.Table<Professor>();
                    List<Professor> list = data.ToList();
                    List<string> listToReturn = new List<string>();
                    foreach (Professor item in list)
                    {
                        listToReturn.Add(item.username);
                    }
                    return listToReturn;
                }
                catch (Exception e)
                {
                    return new List<string>();
                }
            }
        }*/
    }   
}
