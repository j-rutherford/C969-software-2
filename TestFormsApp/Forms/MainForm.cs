using C969App.Data;
using System;
using System.Windows.Forms;

namespace C969App.Forms
{
    public partial class MainForm : Form
    {

        private readonly CustomerRepository _customerRepository;
        private readonly AppointmentRepository _appointmentRepository;

        public MainForm(
            CustomerRepository customerRepository,
            AppointmentRepository appointmentRepository)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
            _appointmentRepository = appointmentRepository;
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
            var appointmentForm = new AppointmentForm(_appointmentRepository);
            appointmentForm.Show();
        }
    }
}
