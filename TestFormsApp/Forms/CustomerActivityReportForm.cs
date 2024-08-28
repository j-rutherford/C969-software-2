using System;
using System.Windows.Forms;
using C969App.Data;
using System.Linq;
using System.Data;

namespace C969App.Forms
{
    public partial class CustomerActivityReportForm : Form
    {
        private readonly CustomerRepository _customerRepository;
        private readonly AppointmentRepository _appointmentRepository;

        public CustomerActivityReportForm(CustomerRepository customerRepository, AppointmentRepository appointmentRepository)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
            _appointmentRepository = appointmentRepository;
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            var customers = _customerRepository.GetAllCustomers();
            var appointments = _appointmentRepository.GetAllAppointments();

            var customerActivity = from customer in customers
                                   join appointment in appointments.AsEnumerable()
                                   on customer.CustomerId equals appointment.Field<int>("CustomerId") into customerAppointments
                                   select new
                                   {
                                       CustomerName = customer.CustomerName,
                                       NumberOfAppointments = customerAppointments.Count(),
                                       NumberOfMissedAppointments = 0, // Placeholder, implement logic for missed appointments if needed
                                       NumberOfRescheduledAppointments = 0 // Placeholder, implement logic for rescheduled appointments if needed
                                   };

            dgvCustomerActivity.DataSource = customerActivity.ToList();
        }
    }
}
