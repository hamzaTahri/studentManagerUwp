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
        public Student MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as Student; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

  


        /**************** delete Student ************************/
        public static bool Delete_Student(string cin)
        {

            var sqlpath = "Data Source="+ Windows.Storage.ApplicationData.Current.LocalFolder.Path+ "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
                {
                    connection.Open();
                    string req = "delete from Students where cin='" + cin + "'";
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


        public static bool Update_Student(string cin,string cin_mod,string fullname, string email,string phone )
        {

            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "update Students set  fullName='"+fullname+"', email='"+email+ "',cin='" + cin_mod + "' , tel='" + phone+"' where cin='" + cin + "'";
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



        public static bool Insert_Student(string cin, int id_field, string fullname, string email, string phone)
        {

            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "insert into Students (fullName,email,cin,tel,fieldId) values  (fullName='" + fullname + "', email='" + email + "',cin='" + cin + "' , tel='" + phone + "' , fieldId='" + id_field + "')";
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





        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(StudentsDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public StudentsDetailControl()
        {


            InitializeComponent();


    
        }

    private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as StudentsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }

        private void BtnRemoveProf_Click(object sender, RoutedEventArgs e)
        {

           if(Delete_Student(MasterMenuItem.Cin.ToString()))
            {
                var messageDialog = new MessageDialog("Student deleted successfully");
                messageDialog.ShowAsync();
                
            }else
            {
                var messageDialog = new MessageDialog("Error !! Please Check Your Information");
                messageDialog.ShowAsync();
            }



        }

        private void BtnEditProf_Click(object sender, RoutedEventArgs e)
        {
            if (Update_Student(MasterMenuItem.Cin,cin.Text,name.Text,email.Text,tel.Text))
            {
                var messageDialog = new MessageDialog("Student updateds successfully");
                messageDialog.ShowAsync();

            }
            else
            {
                var messageDialog = new MessageDialog("Error !! Please Check Your Information");
                messageDialog.ShowAsync();
            }
        }

        private void Button_ClickAsync(object sender, RoutedEventArgs e)
        {

                if(Insert_Student(cin.Text,1, name.Text, email.Text, tel.Text))
                {
                    var messageDialog = new MessageDialog("Student inserted successfully");
                    messageDialog.ShowAsync();
                }
            
        }
    }
}
