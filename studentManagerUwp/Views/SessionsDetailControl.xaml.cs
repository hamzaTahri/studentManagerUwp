﻿using System;

using studentManagerUwp.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace studentManagerUwp.Views
{
    public sealed partial class SessionsDetailControl : UserControl
    {
        public SampleOrder MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as SampleOrder; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(SessionsDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public SessionsDetailControl()
        {
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as SessionsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
