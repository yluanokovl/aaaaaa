using WpfApp9.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp9.ViewModel
{
    public class MainViewModel: ObservableObject
    {
        public ObservableCollection<Contact> Contacts { get; }
        
        private string _name = string.Empty;
        private string _phone = string.Empty;
        private string _errMsg = string.Empty;
        private Contact? _selectedContact;
        private int id = 0;

        public string ErrorMsg 
        { 
            get => _errMsg;
            set => Set(ref _errMsg, value);
        }
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }


        // Команды
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public MainViewModel()
        {
            Contacts = new ObservableCollection<Contact>();
            AddCommand = new RelayCommand(
            AddContact,

            () => CanAddContact());

            DeleteCommand = new RelayCommand(
            DeleteContact,

            () => CanDeleteContact());
        }
        private void AddContact()
        {
            Contact c = new Contact(id++, _name, _phone);
            if (c.Validate())
            {
                Contacts.Add(c);
                Name = string.Empty;
                Phone = string.Empty;
                ErrorMsg = string.Empty;
            }
            else
            {
                ErrorMsg = $"Invalid info";
            }
        }
        private bool CanAddContact()
        {
            return (!string.IsNullOrEmpty(_name) && !string.IsNullOrEmpty(_phone));
        }
        private void DeleteContact()
        {
            if (SelectedContact is not null && Contacts.Contains(SelectedContact))
                Contacts.Remove(SelectedContact);
        }
        private bool CanDeleteContact()
        {
            return (SelectedContact is not null); 
        }
    }
}
