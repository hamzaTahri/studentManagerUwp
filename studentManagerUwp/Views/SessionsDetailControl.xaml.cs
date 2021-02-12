using System;
using System.Collections.ObjectModel;
using studentManagerUwp.Core.Models;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            await DatabaseConnector.LoadRecordsAsync(Fields);
            await DatabaseConnector.LoadRecordsAsync(Courses);
        }

        public void fillDatePicker()
        {
            if (MasterMenuItem != null)
            {
                calendarDatePicker.Date = MasterMenuItem.date;
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
    }
}
