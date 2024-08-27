using System;
using System.Windows.Forms;
using C969App.Data;
using C969App.Forms;

namespace C969App
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var dbContext = new DbContext())
            {
                var userRepository = new UserRepository(dbContext);
                var customerRepository = new CustomerRepository(dbContext);
                var appointmentRepository = new AppointmentRepository(dbContext);

                var loginForm = new LoginForm(userRepository, () => new MainForm(customerRepository, appointmentRepository));
                Application.Run(loginForm);
            }
        }
    }
}
