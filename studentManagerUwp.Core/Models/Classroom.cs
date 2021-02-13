
using SQLite;
using System;
using System.Collections.Generic;

namespace studentManagerUwp
{
    class Classroom
    {


        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        /* public Classroom()
         {
             db.createDatabase();
         }

         public bool Insert_Classroom(Classroom Classroom)
         {
             try
             {
                 using (var connection = db.GetConnection())
                 {
                     connection.Insert(Classroom);
                     ClassroomesAlbum.mBuiltInClassroomes =  new Classroom().allClassrooms().ToArray();
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


         public bool Is_Exist_Classroom(string name)
         {
             try
             {
                 using (var connection = db.GetConnection())
                 {
                     var data = connection.Table<Classroom>();
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
         public List<Classroom> allClassrooms()
         {
             using (var connection = db.GetConnection())
             {
                 try
                 {
                     var data = connection.Table<Classroom>();
                     return data.ToList();
                 }
                 catch(Exception e)
                 {
                     var messageDialog = new MessageDialog(e.Message);
                     messageDialog.ShowAsync();
                     return new List<Classroom>();
                 }
             }
         }
     }

     class ClassroomesAlbum
     {

         public static Classroom[] mBuiltInClassroomes = new Classroom().allClassrooms().ToArray();

         // Array of Classroomes that make up the album:
         private Classroom[] mClassroomes;

         // Create an instance copy of the built-in Classroom list and
         public ClassroomesAlbum()
         {
             mClassroomes = mBuiltInClassroomes;
         }
         // Return the number of Classroomes in the Classroom album:
         public int NumClassroomes
         {
             get { return mClassroomes.Length; }
         }
         // Indexer (read only) for accessing a Classroom:
         public Classroom this[int i]
         {
             get { return mClassroomes[i]; }
         }
     }*/
    }
}
