using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace ExamplePhoneBook
{
    public class SQLiteHelper
    {
        private string connectionString;

        public SQLiteHelper(string dbFilePath)
        {
            connectionString = $"Data Source={dbFilePath};Version=3;";
        }

        public void CreateContactTable()
        {
            string query = @"
            CREATE TABLE IF NOT EXISTS Contacts (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT,
                email TEXT,
                phoneNumber TEXT,
                groups TEXT
            );";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddContact(string name, string email, string phoneNumber, string groups)
        {
            string query = @"
            INSERT INTO Contacts (name, email, phoneNumber, groups) 
            VALUES (@name, @email, @phoneNumber, @groups);";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@groups", groups);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateContact(int id, string name, string email, string phoneNumber, string groups)
        {
            string query = @"
            UPDATE Contacts 
            SET name = @name, email = @email, 
                phoneNumber = @phoneNumber, groups = @groups 
            WHERE id = @id;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@groups", groups);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteContact(int id)
        {
            string query = "DELETE FROM Contacts WHEcd ";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        // Метод для получения информации о конкретном контакте по его идентификатору
        public Contact GetContact(int id)
        {
            Contact contact = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Contacts WHERE id = @id;";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            contact = new Contact
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email= reader.GetString(2),
                                PhoneNumber = reader.GetString(3),
                                Groups = reader.GetString(4)
                            };
                        }
                    }
                }
            }

            return contact;

        } 
        public List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Contacts;";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email= reader.GetString(2),
                                PhoneNumber = reader.GetString(3),
                                Groups = reader.GetString(4)
                            });
                        }
                    }
                }
            }

            return contacts;

        }
        public List<Contact> GetContacts(string value)
        {
            List<Contact> contacts = new List<Contact>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Contacts " +
                    "WHERE name LIKE @value " +
                    "OR email LIKE @value OR phoneNumber LIKE @value OR groups LIKE @value;";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@value", "%"+value+"%");

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email= reader.GetString(2),
                                PhoneNumber = reader.GetString(3),
                                Groups = reader.GetString(4)
                            });
                        }
                    }
                }
            }

            return contacts;

        }
     
    }
}
