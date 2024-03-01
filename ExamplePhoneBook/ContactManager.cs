using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ExamplePhoneBook
{
    public class ContactManager
    {
        private SQLiteHelper sqliteHelper;

        public ContactManager()
        {
            sqliteHelper = new SQLiteHelper(@"test.db");
            sqliteHelper.CreateContactTable(); // Создаем таблицу контактов при инициализации
        }

        public void AddContact(Contact contact)
        {
            sqliteHelper.AddContact(contact.Name, contact.Email, contact.PhoneNumber, contact.Groups);
        }

        public void EditContact(int id, Contact contact)
        {
            sqliteHelper.UpdateContact(id, contact.Name, contact.Email, contact.PhoneNumber, contact.Groups);
        }

        public void DeleteContact(int id)
        {
            sqliteHelper.DeleteContact(id);
        }
        public Contact GetContact(int id)
        {
            return sqliteHelper.GetContact(id);
        }

        public List<Contact> GetContacts()
        {
            return sqliteHelper.GetContacts();
        }
        public List<Contact> GetContacts(string value)
        {
            return sqliteHelper.GetContacts(value);
        }
    }
}
