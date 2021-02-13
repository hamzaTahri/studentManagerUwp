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


        //*******************************************************************************************************************
        private async void BtnInsertField_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Add This As a New Field [" + txtName.Text + " ]");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Insert)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Insert)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();

        }

        private async void BtnRemoveField_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Remove This Field [" + txtName.Text + " ]");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Delete)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Delete)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        private async void BtnEditField_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Update This Field ");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Update)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Update)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        //*******************************************************************************************************************

        public void CommandInvokedHandler_Insert(IUICommand command)
        {
            if (command.Label == "Yes")
            {
                Field f = new Field();
                f.name = txtName.Text;
                f.year = Convert.ToInt32(txtYear.Text);
                f.description = txtDescription.Text;
                if (insertField(f))
                {
                    ProfessorsDetailControl.showOperationDone("Inserted", " Field ");
                }
                else
                {
                    ProfessorsDetailControl.showOperationFailed("Insertion ");
                }
            }
        }
        public void CommandInvokedHandler_Update(IUICommand command)
        {
            if (command.Label == "Yes")
            {
                MasterMenuItem.name = txtName.Text;
                MasterMenuItem.year = Convert.ToInt32(txtYear.Text);
                MasterMenuItem.description = txtDescription.Text;
                if (editField(MasterMenuItem))
                {
                    ProfessorsDetailControl.showOperationDone("Updated", " Field ");
                }
                else
                {
                    ProfessorsDetailControl.showOperationFailed("Updating ");
                }
            }
        }
        public void CommandInvokedHandler_Delete(IUICommand command)
        {
            if (command.Label == "Yes")
            {
                if (deleteField(MasterMenuItem))
                {
                    ProfessorsDetailControl.showOperationDone("Deleted", " Field ");
                }
                else
                {
                    ProfessorsDetailControl.showOperationFailed("Deleting ");
                }
            }
        }

        //*******************************************************************************************************************
        public bool insertField(Field f)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "insert into Fields (name,year,description) values  ('" + f.name + "','" + f.year + "','" + f.description + "')";
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
        public bool editField(Field f)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "update Fields set  name='" + f.name + "', year='" + f.year + "',description='" + f.description + "' where id='" + f.Id + "'";
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
        public bool deleteField(Field f)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "delete from Fields where id='" + f.Id + "'";
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
