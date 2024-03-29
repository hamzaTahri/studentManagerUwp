﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp.UI.Controls;

using studentManagerUwp.Core.Models;
using studentManagerUwp.Core.Services;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

using studentManagerUwp.Core.Models;

namespace studentManagerUwp.Views
{
    public sealed partial class SessionsPage : Page, INotifyPropertyChanged
    {
        private Session _selected;

        public Session Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        ObservableCollection<Session> SampleItems { get; set; }

        public SessionsPage()
        {
            InitializeComponent();
            Loaded += SessionsPage_Loaded;
            SampleItems = new ObservableCollection<Session>();
        }

        private async void SessionsPage_Loaded(object sender, RoutedEventArgs e)
        {
            SampleItems.Clear();

            await DatabaseConnector.LoadRecordsAsync(SampleItems);


            if (MasterDetailsViewControl.ViewState == MasterDetailsViewState.Both)
            {
                Selected = SampleItems.FirstOrDefault();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
