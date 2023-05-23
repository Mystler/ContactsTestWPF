using ContactsDemo.Locales;
using ContactsDemo.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Data;

namespace ContactsDemo.ViewModels
{
    public class ContactsOverview : ViewModelBase
    {
        public ContactsOverview()
        {
            Contacts = new();
            _contactsView = CollectionViewSource.GetDefaultView(Contacts);

            LoadContacts();
            RefreshView();
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        private ICollectionView _contactsView;
        public ICollectionView ContactsView
        {
            get { return _contactsView; }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set { SetProperty(ref _searchText, value); ContactsView.Refresh(); }
        }

        private bool Filter(object contact)
        {
            return String.IsNullOrEmpty(SearchText) || ((Contact)contact).Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Rebuild the filtered and sorted view after adding or removing elements.
        /// </summary>
        public void RefreshView()
        {
            _contactsView = CollectionViewSource.GetDefaultView(Contacts);
            ContactsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            ContactsView.Filter = Filter;
            ContactsView.Refresh();
        }

        // Hardcoded. Ideally this should be exposed as part of some settings interface or moved into some constants class if we have others.
        private const string DB_FILE = "Contacts.json";

        /// <summary>
        /// Use to load a simple database of Contacts from a JSON File. We use a static variable specifying the path.
        /// This could be parameterized and extended with move operations if the app had a settings menu.
        /// </summary>
        public void LoadContacts()
        {
            // No file existing yet, proceed with current list.
            if (!File.Exists(DB_FILE))
                return;
            try
            {
                using var stream = new FileStream(DB_FILE, FileMode.Open, FileAccess.Read);
                Contacts = JsonSerializer.Deserialize<ObservableCollection<Contact>>(stream) ?? Contacts;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Locale.LoadException);
            }
        }

        /// <summary>
        /// Save current contacts list to the DB JSON file.
        /// </summary>
        public void SaveContacts()
        {
            // No contacts yet, so no need to write a new file.
            if (!File.Exists(DB_FILE) && Contacts.Count == 0)
                return;
            File.WriteAllText(DB_FILE, JsonSerializer.Serialize(Contacts));
        }
    }
}
