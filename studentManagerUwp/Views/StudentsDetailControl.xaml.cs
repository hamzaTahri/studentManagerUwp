using System;

using studentManagerUwp.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace studentManagerUwp.Views
{
    public sealed partial class StudentsDetailControl : UserControl
    {
        public Student MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as Student; }
            set { SetValue(MasterMenuItemProperty, value); }
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
    }
}
