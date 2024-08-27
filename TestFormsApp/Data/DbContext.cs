using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace C969App.Data
{
    public class DbContext : IDisposable
    {
        private readonly string connectionString;
        private MySqlConnection connection;

        public DbContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        public MySqlConnection Connection => connection;

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
