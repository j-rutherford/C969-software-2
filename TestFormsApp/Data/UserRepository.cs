using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using C969App.Models;

namespace C969App.Data
{
    public class UserRepository
    {
        private readonly DbContext _context;

        public UserRepository(DbContext context)
        {
            _context = context;
        }

        public (bool isAuthenticated, int userId) AuthenticateUser(string username, string password)
        {
            string query = "SELECT UserId FROM user WHERE Username = @username AND Password = @password";
            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                // Execute the command and attempt to retrieve the UserId
                var result = command.ExecuteScalar();

                // Check if a valid UserId was returned
                if (result != null && int.TryParse(result.ToString(), out int userId))
                {
                    return (true, userId); // Return true if authenticated, along with the UserId
                }
                else
                {
                    return (false, 0); // Return false if not authenticated, with UserId as 0
                }
            }
        }


        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            string query = "SELECT * FROM user";
            using (var command = new MySqlCommand(query, _context.Connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var user = new User
                    {
                        UserId = reader.GetInt32("UserId"),
                        Username = reader.GetString("Username"),
                        Password = reader.GetString("Password"),
                        Active = reader.GetBoolean("Active"),
                        CreateDate = reader.GetDateTime("CreateDate"),
                        CreatedBy = reader.GetString("CreatedBy"),
                        LastUpdate = reader.GetDateTime("LastUpdate"),
                        LastUpdateBy = reader.GetString("LastUpdateBy")
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        // Additional CRUD methods...
    }
}
