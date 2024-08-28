using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace C969App.Data
{
    public class DbContext : IDisposable
    {
        private readonly string connectionString;
        private MySqlConnection _connection;

        public DbContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }

        //Lambda used for expression body, saves space. Returns the private connection.
        public MySqlConnection Connection => _connection;

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
