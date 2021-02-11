using System;

using studentManagerUwp.Core.Models;

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
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProfessorsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
