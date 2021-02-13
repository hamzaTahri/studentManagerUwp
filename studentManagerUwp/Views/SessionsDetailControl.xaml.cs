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

        static ObservableCollection<Student> Students { get; set; }
        static ObservableCollection<StudentSession> StudentSessions { get; set; }

        Field CurrentField { get; set; }
        Course CurrentCourse { get; set; }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(Session), typeof(SessionsDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public SessionsDetailControl()
        {
            InitializeComponent();
            Fields = new ObservableCollection<Field>();
            Courses = new ObservableCollection<Course>();
            Students = new ObservableCollection<Student>();
            StudentSessions = new ObservableCollection<StudentSession>();

            CurrentField = new Field();
            CurrentCourse = new Course();
            FillCombos();
        }

        private static bool studentPresent(int studentId, int sessionId)
        {
            foreach (StudentSession ss in StudentSessions)
            {
                if (ss.studentId == studentId && ss.sessionId == sessionId)
                {
                    return true;
                }
            }
            return false;
        }

        static ObservableCollection<Student> presentStudents = new ObservableCollection<Student>();
        static ObservableCollection<Student> absentStudents = new ObservableCollection<Student>();

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SessionsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);

            try
            {
                control.listViewPresents.Items.Clear();
                control.listViewAbsents.Items.Clear();
                presentStudents.Clear();
                absentStudents.Clear();

                Students = getStudentsOfField(control.MasterMenuItem.fieldId);
                foreach (Student st in Students)
                {
                    if (studentPresent(st.Id, control.MasterMenuItem.Id))
                    {
                        control.listViewPresents.Items.Add(st.FullName);
                        presentStudents.Add(st);
                    }
                    else
                    {
                        control.listViewAbsents.Items.Add(st.FullName);
                        absentStudents.Add(st);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

        private static  ObservableCollection<Student> getStudentsOfField(int fieldId)
        {
            FillCombos2();
            ObservableCollection<Student> studentsOfField = new ObservableCollection<Student>();
            foreach(Student st in Students)
            {
                if(st.FieldId == fieldId)
                {
                    studentsOfField.Add(st);
                }
            }
            return studentsOfField;
        }

        public async void FillCombos()
        {
            await DatabaseConnector.LoadRecordsAsyncForField(Fields);
            await DatabaseConnector.LoadRecordsAsyncForCourse(Courses);
            await DatabaseConnector.LoadRecordsAsyncForStudent(Students);
            await DatabaseConnector.LoadRecordsAsyncForStudentSession(StudentSessions);
        }
        public static async void FillCombos2()
        {
            await DatabaseConnector.LoadRecordsAsyncForStudent(Students);
            await DatabaseConnector.LoadRecordsAsyncForStudentSession(StudentSessions);
        }

        public void fillDatePicker()
        {
            if (MasterMenuItem != null)
            {
                calendarDatePicker.Date = DateTime.Parse(MasterMenuItem.date);
            }
        }





        public void FillSelected()
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
        //*******************************************************************************
        private async void listViewAbsents_ItemClick(object sender, ItemClickEventArgs e)
        {

            ObservableCollection<Student> newAbsentStudents = new ObservableCollection<Student>();
            foreach (Student st in absentStudents)
            {
                if (st.FullName != e.ClickedItem.ToString())
                {
                    newAbsentStudents.Add(st);
                }
                else
                {
                    presentStudents.Add(st);
                }
            }
            absentStudents = newAbsentStudents;
            refillStudentsListView(absentStudents, presentStudents);
        }

        private void listViewPresents_ItemClick(object sender, ItemClickEventArgs e)
        {
            ObservableCollection<Student> newPresentStudents = new ObservableCollection<Student>();
            foreach (Student st in presentStudents)
            {
                if (st.FullName != e.ClickedItem.ToString())
                {
                    newPresentStudents.Add(st);
                }
                else
                {
                    absentStudents.Add(st);
                }
            }
            presentStudents = newPresentStudents;
            refillStudentsListView(absentStudents, presentStudents);
        }
        public void refillStudentsListView(ObservableCollection<Student> absents, ObservableCollection<Student> presents)
        {
            listViewAbsents.Items.Clear();
            foreach (Student st in absents)
            {
                listViewAbsents.Items.Add(st.FullName);
            }
            listViewPresents.Items.Clear();
            foreach (Student st in presents)
            {
                listViewPresents.Items.Add(st.FullName);
            }
        }

        private async void BtnSaveAbsence_Click(object sender, RoutedEventArgs e)
        {

            var messageDialog = new MessageDialog("Do You Really Want To Insert This as a new Session");
#pragma warning disable UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandInvokedHandler_Save)));
#pragma warning restore UWP003 // UWP-only
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandInvokedHandler_Save)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();


            //Sure To Save
            /*try
            {
                deleteAllStudentSessionIds(MasterMenuItem);
                insertNewStudentSessions(MasterMenuItem, presentStudents);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }*/

        }

        private void CommandInvokedHandler_Save(IUICommand command)
        {
            if (command.Label == "Yes")
            {
                //Sure To Save
                try
                {
                    deleteAllStudentSessionIds(MasterMenuItem);
                    if(insertNewStudentSessions(MasterMenuItem, presentStudents))
                    {
                        ProfessorsDetailControl.showOperationDone(" Saved ", " Attendance ");
                    }
                    else
                    {
                        ProfessorsDetailControl.showOperationFailed("Saving Attendance ");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void deleteAllStudentSessionIds(Session s)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                string req = "delete from StudentSessions where sessionId='" + s.Id + "'";
                SQLiteCommand command = new SQLiteCommand(req, connection);
                var reader = command.ExecuteNonQuery();

                if (reader > 0)
                {
                    connection.Close();
                    return ;
                }
                else
                {
                    connection.Close();
                    return ;
                }
            }
        }
        private bool insertNewStudentSessions(Session s, ObservableCollection<Student> presents)
        {
            var sqlpath = "Data Source=" + Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\studentManagerDatabase.db";
            using (SQLiteConnection connection = new SQLiteConnection(sqlpath))
            {
                connection.Open();
                try
                {
                    string req;
                    SQLiteCommand command;
                    foreach (Student st in presents)
                    {
                        req = "insert Into StudentSessions (sessionId,studentId) Values('" + s.Id + "','" + st.Id + "')";
                        command = new SQLiteCommand(req, connection);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    connection.Close();
                    return false;
                }
            }

        }
    }
}

