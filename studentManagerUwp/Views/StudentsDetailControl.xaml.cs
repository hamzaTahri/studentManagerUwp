using System;
using System.IO;
using System.Linq;
using studentManagerUwp.Core.Models;
using studentManagerUwp.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Collections.ObjectModel;

namespace studentManagerUwp.Views
{
    public sealed partial class StudentsDetailControl : UserControl
    {
        Field CurrentField { get; set; }
        ObservableCollection<Field> Fields { get; set; }


        public Student MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as Student; }
            set { SetValue(MasterMenuItemProperty, value); }
        }




        /**************** delete Student ************************/
        public static bool Delete_Student(string cin)
        {

            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "delete from Students where cin='" + cin + "'";
                SQLiteCommand command = new SQLiteCommand(req, connection);
                var reader = command.ExecuteNonQuery();

                if (reader > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }


        public static bool Update_Student(string cin, string cin_mod, string fullname, string email, string phone, int fieldId)
        {

            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "update Students set  fullName='" + fullname + "', email='" + email + "',cin='" + cin_mod + "' , tel='" + phone + "',fieldId='"+fieldId+"' where cin='" + cin + "'";
                SQLiteCommand command = new SQLiteCommand(req, connection);

                if (command.ExecuteNonQuery() > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }



        }



        public static bool Insert_Student(string cin, int id_field, string fullname, string email, string phone, int fieldId)
        {

            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "insert into Students (fullName,email,cin,tel,fieldId) values  ('" + fullname + "', '" + email + "','" + cin + "' , '" + phone + "' , '" + fieldId + "')";
                SQLiteCommand command = new SQLiteCommand(req, connection);
                var reader = command.ExecuteNonQuery();

                if (reader > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
        }





        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(StudentsDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public StudentsDetailControl()
        {


            InitializeComponent();

            CurrentField = new Field();
            Fields = new ObservableCollection<Field>();

            FillCombos();

        }

        public async void FillCombos()
        {
            await DatabaseConnector.LoadRecordsAsyncForField(Fields);
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as StudentsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }

        private async void BtnRemoveStudent_Click(object sender, RoutedEventArgs e)
        {

            if (Delete_Student(MasterMenuItem.Cin.ToString()))
            {
                var messageDialog = new MessageDialog("Student deleted successfully");
                await messageDialog.ShowAsync();

            }
            else
            {
                var messageDialog = new MessageDialog("Error !! Please Check Your Information");
                await messageDialog.ShowAsync();
            }



        }

        private async void BtnEditStudent_Click(object sender, RoutedEventArgs e)
        {
            if (Update_Student(MasterMenuItem.Cin, cin.Text, name.Text, email.Text, tel.Text , CurrentField.Id))
            {
                var messageDialog = new MessageDialog("Student updateds successfully");
                messageDialog.ShowAsync();

            }
            else
            {
                var messageDialog = new MessageDialog("Error !! Please Check Your Information");
               await messageDialog.ShowAsync();
            }
        }

        private async void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {

            if (Insert_Student(cin.Text, 1, name.Text, email.Text, tel.Text, CurrentField.Id))
            {
                var messageDialog = new MessageDialog("Student inserted successfully");
                await messageDialog.ShowAsync();
            }

        }

        public string getSelectedFieldName(int fieldId)
        {
            foreach (Field f in Fields)
            {
                if (f.Id == fieldId)
                {
                    return f.name;
                }
            }
            return null;
        }
    }
}
