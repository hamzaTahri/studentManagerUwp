using System;

using studentManagerUwp.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace studentManagerUwp.Views
{
    public sealed partial class CoursesDetailControl : UserControl
    {
        public Course MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as Course; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(CoursesDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public CoursesDetailControl()
        {
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CoursesDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
