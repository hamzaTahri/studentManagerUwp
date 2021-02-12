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

namespace studentManagerUwp.Views
{
    public sealed partial class StudentsPage : Page, INotifyPropertyChanged
    {
        private Student _selected;

        public Student Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        ObservableCollection<Student> SampleItems { get; set; }
        public StudentsPage()
        {
            SampleItems = new ObservableCollection<Student>();
            InitializeComponent();
            Loaded += StudentsPage_Loaded;
        }

        private async void StudentsPage_Loaded(object sender, RoutedEventArgs e)
        {
            SampleItems.Clear();


            await DatabaseConnector.LoadRecordsAsyncForStudent(SampleItems);

            if (MasterDetailsViewControl.ViewState == MasterDetailsViewState.Both)
            {
                //Selected = SampleItems.FirstOrDefault();

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
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
