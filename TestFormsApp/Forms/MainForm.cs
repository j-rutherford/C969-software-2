using C969App.Data;
using System;
using System.Windows.Forms;

namespace C969App.Forms
{
    public partial class MainForm : Form
    {

        private readonly CustomerRepository _customerRepository;
        private readonly AppointmentRepository _appointmentRepository;
        private readonly UserRepository _userRepository;

        public MainForm(
            CustomerRepository customerRepository,
            AppointmentRepository appointmentRepository,
            UserRepository userRepository)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
            this.FormClosing += MainForm_FormClosing;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            var customerForm = new CustomerForm(_customerRepository);
            customerForm.Show();
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            var appointmentForm = new AppointmentForm(_appointmentRepository,_customerRepository, _userRepository);
            appointmentForm.Show();
        }
        private void btnReports_Click(object sender, EventArgs e)
        {
            var ReportForm = new ReportForm(_appointmentRepository, _userRepository, _customerRepository);
            ReportForm.Show();
        }
    }
}
