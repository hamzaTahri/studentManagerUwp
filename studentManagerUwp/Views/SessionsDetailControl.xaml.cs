using System;
using System.Data.SQLite;
using studentManagerUwp.Core.Models;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace studentManagerUwp.Views
{
    public sealed partial class SessionsDetailControl : UserControl
    {
        public Session MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as Session; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        ObservableCollection<Field> Fields { get; set; }
        ObservableCollection<Course> Courses { get; set; }

        Field CurrentField { get; set; }
        Course CurrentCourse { get; set; }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(Session), typeof(SessionsDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public SessionsDetailControl()
        {
            InitializeComponent();
            Fields = new ObservableCollection<Field>();
            Courses = new ObservableCollection<Course>();

            CurrentField = new Field();
            CurrentCourse = new Course();
            FillCombos();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SessionsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);

        }

        public async void FillCombos()
        {
            await DatabaseConnector.LoadRecordsAsyncForField(Fields);
            await DatabaseConnector.LoadRecordsAsyncForCourse(Courses);
        }

        public void fillDatePicker()
        {
            if (MasterMenuItem != null)
            {
                calendarDatePicker.Date = DateTime.Parse(MasterMenuItem.date);
            }
        }





        public async void FillSelected()
        {
            if (MasterMenuItem != null)
            {

                foreach (Course c in Courses)
                {
                    if (c.Id == MasterMenuItem.courseId)
                    {
                        CurrentCourse = c;
                        break;
                    }
                }
            }
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            throw new NotImplementedException();
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

        public Course getSelectedCourse(int courseId)
        {
            foreach (Course c in Courses)
            {
                if (c.Id == courseId)
                {
                    return c;
                }
            }
            return null;
        }
        public string getSelectedCourseName(int courseId)
        {
            foreach (Course c in Courses)
            {
                if (c.Id == courseId)
                {
                    return c.name;
                }
            }
            return null;
        }

        //---------------------------------------------------------------------------------------------------------------------------------
        private async void BtnAddSession_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Insert This as a new Session");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Insert)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Insert)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }


        private async void BtnRemoveSession_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Remove This Session");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Delete)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Delete)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        private async void BtnEditSession_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Update This Session");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Update)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Update)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }


        //---------------------------------------------------------------------------------------------------------------------------------
        public bool insertSession(Session s)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "insert into Sessions (date,startTime,endTime,fieldId,courseId) values  ('" + s.date + "','" + s.startTime + "','" + s.endTime + "','" + s.fieldId + "','" + s.courseId + "')";
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
        public bool editSession(Session s)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "update Sessions set date='" + s.date + "',startTime='" + s.startTime + "',endTime='" + s.endTime + "',fieldId='" + s.fieldId + "',courseId='" + s.courseId + "' where id='" + s.Id + "'";
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
        public bool deleteSession(Session s)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "delete from Sessions where id='" + s.Id + "'";
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

        //-----------------------------------------------------------------------------------------------------------------------
        void CommandInvokedHandler_Insert(IUICommand command)
        {
            if (command.Label == "Yes")
            {
                Session session = new Session();
                session.date = calendarDatePicker.Date.Value.DateTime.ToShortDateString();
                session.startTime = startTimePicker.Time.Hours + " : " + startTimePicker.Time.Minutes;
                session.endTime = endTimePicker.Time.Hours + " : " + endTimePicker.Time.Minutes;
                session.fieldId = CurrentField.Id;
                session.courseId = CurrentCourse.Id;
                if (insertSession(session))
                {
                    ProfessorsDetailControl.showOperationDone("Inserted", "Session");
                }
                else
                {
                    ProfessorsDetailControl.showOperationFailed("Insertion ");
                }
            }
        }
        void CommandInvokedHandler_Update(IUICommand command)
        {
            if (command.Label == "Yes")
            {
                MasterMenuItem.date = calendarDatePicker.Date.Value.Date.ToShortDateString();
                MasterMenuItem.startTime = startTimePicker.Time.Hours + " : " + startTimePicker.Time.Minutes;
                MasterMenuItem.endTime = endTimePicker.Time.Hours + " : " + endTimePicker.Time.Minutes;
                MasterMenuItem.fieldId = CurrentField.Id;
                MasterMenuItem.courseId = CurrentCourse.Id;

                if (editSession(MasterMenuItem))
                {
                    ProfessorsDetailControl.showOperationDone("Updated", "Session");
                }
                else
                {
                    ProfessorsDetailControl.showOperationFailed("Updating ");
                }
            }
        }
        void CommandInvokedHandler_Delete(IUICommand command)
        {
            if (command.Label == "Yes")
            {
                if (deleteSession(MasterMenuItem))
                {
                    ProfessorsDetailControl.showOperationDone(" Deleted ", " Session ");
                }
                else
                {
                    ProfessorsDetailControl.showOperationFailed("Deleting ");
                }
            }
        }

    }
}

