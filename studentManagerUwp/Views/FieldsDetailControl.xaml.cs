using System;

using studentManagerUwp.Core.Models;

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
    }
}
