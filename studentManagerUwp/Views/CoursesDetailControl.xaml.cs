using System;
using System.Data.SQLite;
using studentManagerUwp.Core.Models;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Popups;

namespace studentManagerUwp.Views
{
    public sealed partial class CoursesDetailControl : UserControl
    {

        ObservableCollection<Field> Fields { get; set; }
        ObservableCollection<Professor> Profs { get; set; }

        Field CurrentField { get; set; }
        Professor CurrentProf { get; set; }


        public Course MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as Course; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(CoursesDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public CoursesDetailControl()
        {
            InitializeComponent();

            Fields = new ObservableCollection<Field>();
            Profs = new ObservableCollection<Professor>();

            CurrentField = new Field();
            CurrentProf = new Professor();
            FillCombos();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CoursesDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }

        private async void BtnRemoveCourse_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Remove This Course");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Delete)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Delete)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        private async void BtnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Add " + txtName.Text +" As a new Course");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Update)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Update)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        private async void BtnEditCourse_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Edit This Course");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Update)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Update)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }


        public void CommandInvokedHandler_Insert(IUICommand command)
        {
            if (command.Label == "Yes")
            {
                Course c = new Course();
                c.name = txtName.Text;
                c.description = txtDescription.Text;
                c.fieldId = CurrentField.Id;
                c.profId = CurrentProf.Id;
                if (insertCourse(c))
                {
                    ProfessorsDetailControl.showOperationDone("Inserted","Course");
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
                MasterMenuItem.description = txtDescription.Text;
                MasterMenuItem.profId = CurrentProf.Id;
                MasterMenuItem.fieldId = CurrentField.Id;
                if (editCourse(MasterMenuItem))
                {
                    ProfessorsDetailControl.showOperationDone("Updated","Course");
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
                if (deleteCourse(MasterMenuItem))
                {
                    ProfessorsDetailControl.showOperationDone(" Deleted ", " Course ");
                }
                else
                {
                    ProfessorsDetailControl.showOperationFailed("Deleting ");
                }
            }
        }


        public string sqlpath;
        public bool insertCourse(Course c)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "insert into Courses (name,description,fieldId,profId) values  ('" + c.name + "','" + c.description + "',"+c.fieldId+","+c.profId+")";
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
        public bool editCourse(Course c)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "update Courses set name='" + c.name + "',description='" + c.description + "',fieldId='" + c.fieldId + "',profId='" + c.profId +"' where id='" + c.Id + "'";
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
        public bool deleteCourse(Course c)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "delete from Courses where id='" + c.Id + "'";
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

        public Professor getSelectedProf(int profId)
        {
            foreach (Professor prof in Profs)
            {
                if (prof.Id == profId)
                {
                    return prof;
                }
            }
            return null;
        }
        public string getSelectedProfName(int profId)
        {
            foreach (Professor prof in Profs)
            {
                if (prof.Id == profId)
                {
                    return prof.fullName;
                }
            }
            return null;
        }

        public Field getSelectedField(int fieldId)
        {
            foreach (Field f in Fields)
            {
                if (f.Id == MasterMenuItem.fieldId)
                {
                    return f;
                }
            }
            return null;
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

        public async void FillCombos()
        {
            await DatabaseConnector.LoadRecordsAsyncForField(Fields);
            await DatabaseConnector.LoadRecordsAsync(Profs);
        }

    }
}
