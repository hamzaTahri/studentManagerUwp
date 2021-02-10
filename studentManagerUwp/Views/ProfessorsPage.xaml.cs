using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp.UI.Controls;

using studentManagerUwp.Core.Models;
using studentManagerUwp.Core.Services;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace studentManagerUwp.Views
{
    public sealed partial class ProfessorsPage : Page, INotifyPropertyChanged
    {
        private Professor _selected;

        public Professor Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

         ObservableCollection<Professor> SampleItems { get; set; }

        public ProfessorsPage()
        {
            SampleItems  = new ObservableCollection<Professor>();
            InitializeComponent();
            Loaded += ProfessorsPage_Loaded;

        }

        private async void ProfessorsPage_Loaded(object sender, RoutedEventArgs e)
        {
            SampleItems.Clear();

            await DatabaseConnector.LoadRecordsAsync(SampleItems);

            foreach (Professor p in SampleItems)
            {
                
            var messageDialog = new MessageDialog(SampleItems.Count+ " Person Data  : " + p.Id + " - " + p.fullName + " - " + p.password);

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Try again",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand(
                "Close",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
            }
        

        



            if (MasterDetailsViewControl.ViewState == MasterDetailsViewState.Both)
            {
                //Selected = SampleItems.FirstOrDefault();
            }
        }
        private void CommandInvokedHandler(IUICommand command)
        {
            // Display message showing the label of the command that was invoked
            //NotifyUser("The '" + command.Label + "' command has been selected.",
                //NotifyType.StatusMessage);
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
