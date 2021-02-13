using System;
using System.Data.SQLite;
using studentManagerUwp.Core.Models;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace studentManagerUwp.Views
{
    public sealed partial class ProfessorsDetailControl : UserControl
    {
        public Professor MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as Professor; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(Professor), typeof(ProfessorsDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public ProfessorsDetailControl()
        {
            InitializeComponent();
            btnAddProf.Click += BtnAddProf_Click;
        }

        private async void BtnAddProf_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Add Person [" + txtName.Text + " , " + txtEmail.Text + " , " + txtPassword.Text + " ]");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Insert)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Insert)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        private async void BtnEditProf_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Modify This Person ");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Update)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Update)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        private async void BtnDeleteProf_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Add Remove This Person");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Delete)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Delete)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        //**********************************************************************************************************
        public void CommandInvokedHandler_Insert(IUICommand command)
        {
            if (command.Label == "Yes")
            {
                Professor p = new Professor();
                p.fullName = txtName.Text;
                p.email = txtEmail.Text;
                p.password = txtPassword.Text;
                if (insertProf(p))
                {
                    showOperationDone("Inserted", "Person");
                }
                else
                {
                    showOperationFailed("Insertion ");
                }
            }
        }
        public void CommandInvokedHandler_Update(IUICommand command)
        {
            if (command.Label == "Yes")
            {
                MasterMenuItem.fullName = txtName.Text;
                MasterMenuItem.email = txtEmail.Text;
                MasterMenuItem.password = txtPassword.Text;
                if (editProf(MasterMenuItem))
                {
                    showOperationDone("Updated", "Person");
                }
                else
                {
                    showOperationFailed("Updating ");
                }
            }
        }
        public void CommandInvokedHandler_Delete(IUICommand command)
        {
            if (command.Label == "Yes")
            {
                if (deleteProf(MasterMenuItem))
                {
                    showOperationDone("Deleted", "Person");
                }
                else
                {
                    showOperationFailed("Deleting ");
                }
            }
        }
        public static async void showOperationDone(string s, string s2)
        {
            var messageDialog = new MessageDialog("New " + s2 + " Has been " + s);

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Ok",
                new UICommandInvokedHandler(CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private static void CommandInvokedHandler(IUICommand command)
        {
        }

        public static async void showOperationFailed(string s)
        {
            var messageDialog = new MessageDialog(s + " Has Failed Please Check Log for more Info");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Cancel",
                new UICommandInvokedHandler(CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProfessorsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }


        //*******************************************************************************************************************
        public bool insertProf(Professor p)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "insert into Professors (fullName,email,password) values  ('" + p.fullName + "','" + p.email + "','" + p.password + "')";
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
        public bool editProf(Professor p)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "update Professors set  fullName='" + p.fullName + "', email='" + p.email + "',password='" + p.password + "' where id='" + p.Id + "'";
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
        public bool deleteProf(Professor p)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "delete from Professors where id='" + p.Id + "'";
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
    }
}
