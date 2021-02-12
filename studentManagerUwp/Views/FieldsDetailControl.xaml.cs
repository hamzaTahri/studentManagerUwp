using System;
using System.Data.SQLite;
using studentManagerUwp.Core.Models;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace studentManagerUwp.Views
{
    public sealed partial class FieldsDetailControl : UserControl
    {
        public Field MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as Field; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static bool Delete_Field(string name)
        {

            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "delete from Fields where name='" + name + "'";
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


        public static bool Update_Field(string name,string name_mod, int year, string description)
        {

            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "update Fields set  name='" + name_mod + "', year='" + year + "',description='" + description + "' where name='" + name + "'";
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



        public static bool Insert_Field(string name, int year, string description)
        {

            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "insert into Fields (name,year,description) values  ('" + name + "','" + year + "','" + description + "')";
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

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(FieldsDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public FieldsDetailControl()
        {
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FieldsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }

        private void BtnRemoveProf_Click(object sender, RoutedEventArgs e)
        {

            if (Delete_Field(MasterMenuItem.name.ToString()))
            {
                var messageDialog = new MessageDialog("Field deleted successfully");
                messageDialog.ShowAsync();

            }
            else
            {
                var messageDialog = new MessageDialog("Field !! Please Check Your Information");
                messageDialog.ShowAsync();
            }
        }

        private void BtnEditProf_Click(object sender, RoutedEventArgs e)
        {
            if (Update_Field(MasterMenuItem.name,name_field.Text.ToString(),Convert.ToInt16(year_field.Text),description_field.Text.ToString()))
            {
                var messageDialog = new MessageDialog("Field updateds successfully");
                messageDialog.ShowAsync();

            }
            else
            {
                var messageDialog = new MessageDialog("Error !! Please Check Your Information");
                messageDialog.ShowAsync();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
    
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Insert_Field(name_field.Text.ToString(), Convert.ToInt16(year_field.Text), description_field.Text.ToString()))
            {
                var messageDialog = new MessageDialog("Field inserted successfully");
                messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog = new MessageDialog("Error !! Please Check Your Information");
                messageDialog.ShowAsync();
            }
        }
    }
}
