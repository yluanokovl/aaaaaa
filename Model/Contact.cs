using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp9.Model
{
    public class Contact : ObservableObject
    {
        private int _id;
        private string _name;
        private string _phone;

        public string Name 
        {
            get { return _name; } 
            set 
            { 
                _name = value.Trim();
                Set(ref _name, value);
            }
        }

        public string Phone
        {
            get { return _phone; }
            set 
            {
                value = value.Trim();
                if (IsPhoneValid(value)) // начинается с +, после + только цифры, 12 символов
                {
                    Set(ref _phone, value);
                }
            }
        }

        public bool Validate()
        {
            return (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Phone) && IsPhoneValid(Phone));
        }

        private bool IsPhoneValid(string phone)
        {
            return (phone.StartsWith('+') && phone.Substring(1, phone.Length - 1).All(char.IsDigit) && phone.Length == 12);
        }

        public Contact(int id, string name, string phone)
        {
            _id = id;
            _name = name;
            _phone = phone;
        }

        public override string ToString()
        {
            return $"{Name} : {Phone}";
        }
    }
}
