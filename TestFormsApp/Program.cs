using System;
using System.Windows.Forms;
using C969App.Data;
using C969App.Forms;
using MySql.Data.MySqlClient;

namespace C969App
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitializeDatabase();
            using (var dbContext = new DbContext())
            {
                var userRepository = new UserRepository(dbContext);
                var customerRepository = new CustomerRepository(dbContext);
                var appointmentRepository = new AppointmentRepository(dbContext);
                //using anonymous function to defer the creation of the main form instance until we actually need it.
                var loginForm = new LoginForm(userRepository, () => new MainForm(customerRepository, appointmentRepository, userRepository),appointmentRepository);
                Application.Run(loginForm);
            }
        }
        private static void InitializeDatabase()
        {
            using (var context = new DbContext())
            {
                // Check if the Users table has any records
                var query = "SELECT COUNT(*) FROM user";
                using (var command = new MySqlCommand(query, context.Connection))
                {
                    var userCount = Convert.ToInt32(command.ExecuteScalar());
                    if (userCount == 0)
                    {
                        // No users found, run the SQL script to populate the database
                        RunSqlScript("../../Data/LoadData.sql", context);
                    }
                }
            }
        }

        private static void RunSqlScript(string scriptPath, DbContext context)
        {
            try
            {
                var script = System.IO.File.ReadAllText(scriptPath);
                using (var command = new MySqlCommand(script, context.Connection))
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Database initialized successfully.", "Initialization", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing the database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
