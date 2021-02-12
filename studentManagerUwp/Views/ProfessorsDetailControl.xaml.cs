using System;

using studentManagerUwp.Core.Models;
using Windows.UI.Popups;
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
            btnAddProf.Click += BtnAddProf_Click;
        }

        private async void BtnAddProf_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Do You Really Want To Add Person [" + txtName.Text + " , " + txtEmail.Text + " , " + txtPassword.Text + " ]");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Yes",
                new UICommandInvokedHandler(CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand(
                "No",
                new UICommandInvokedHandler(CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        public void CommandInvokedHandler(IUICommand command)
        {

            if (command.Label == "Yes")
            {
                Professor p = new Professor();
                p.fullName = txtName.Text;
                p.email = txtEmail.Text;
                p.password = txtPassword.Text;
                p.insertMe();
            }

        }
        public async void showDataInserted()
        {
            var messageDialog = new MessageDialog("New Person Has been Inserted");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Ok",
                new UICommandInvokedHandler(CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProfessorsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
