
using SQLite;
using System;
using System.Collections.Generic;

namespace studentManagerUwp
{
    class Field
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string year { get; set; }
        public string description { get; set; }

      /*  public Field()
        {
            db.createDatabase();
        }

        public bool Insert_Field(Field field)
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    connection.Insert(field);
                    FieldsAlbum.mBuiltInFields = new Field().allFields().ToArray();
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


        public bool Is_Exist_Field(string name)
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    var data = connection.Table<Field>();
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
                    var data = connection.Table<Field>();
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

        public List<Field> allFields()
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    var data = connection.Table<Field>();
                    return data.ToList();
                }
                catch (Exception e)
                {
                    return new List<Field>();
                }
            }
        }

        internal bool editField()
        {
            try
            {
                using (var connection = db.GetConnection())
                {
                    var data = connection.Table<Field>();
                    var data1 = (from values in data
                                 where values.Id == this.Id
                                 select values).FirstOrDefault();
                    data1.name = name;
                    data1.description = description;
                    data1.year = year ;
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

        public List<string> allFieldsNames()
        {
            using (var connection = db.GetConnection())
            {
                try
                {
                    var data = connection.Table<Field>();
                    List<Field> list =  data.ToList();
                    List<string> listToReturn = new List<string>();
                    foreach (Field item in list)
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

        public Field  fieldById(int fieldId)
        {
            using (var connection = db.GetConnection())
            {
                var data = connection.Table<Field>();
                return data.Where(x => x.Id == fieldId).FirstOrDefault();
            }
        }
    }

    class FieldsAlbum
    {

        public static Field[] mBuiltInFields = new Field().allFields().ToArray();

        // Array of Fields that make up the album:
        private Field[] mFields;

        // Create an instance copy of the built-in Classroom list and
        public FieldsAlbum()
        {
            mFields = mBuiltInFields;
        }
        // Return the number of Fields in the Classroom album:
        public int NumFields
        {
            get { return mFields.Length; }
        }
        // Indexer (read only) for accessing a Classroom:
        public Field this[int i]
        {
            get { return mFields[i]; }
        }*/
    }

}
