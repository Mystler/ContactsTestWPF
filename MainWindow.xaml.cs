using ContactsDemo.Models;
using ContactsDemo.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ContactsDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Properties of available viewmodels to set our window data context to.
        // Not using an explicit view model for contacts for now since we bind a DataTemplate to the Model itself and refresh when needed.
        public ContactsOverview ContactsOverview { get; private set; }

        public MainWindow()
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
            InitializeComponent();
            ContactsOverview = new ContactsOverview();
            DataContext = ContactsOverview;
        }

        private void AddContactButton_Click(object sender, RoutedEventArgs e)
        {
            Contact newContact = new();
            ContactsOverview.Contacts.Add(newContact);
            ContactsOverview.RefreshView();
            DataContext = newContact;
        }

        private void EditContactButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = ((Button)sender).DataContext; // Button context will be the contact and call our DataTemplate
        }

        private void ContactEditBackButton_Click(object sender, RoutedEventArgs e)
        {
            ContactsOverview.RefreshView();
            DataContext = ContactsOverview;
            ContactsOverview.SaveContacts();
        }

        private void ContactEditDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ContactsOverview.Contacts.Remove((Contact)DataContext);
            ContactsOverview.RefreshView();
            DataContext = ContactsOverview;
            ContactsOverview.SaveContacts();
        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            ContactsOverview.SearchText = string.Empty;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ContactsOverview.SaveContacts();
        }
    }
}
