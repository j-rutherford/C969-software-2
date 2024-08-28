using System.Windows.Forms;
using System;
using C969App.Data;

namespace C969App.Forms
{
    public partial class ReportForm : Form
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly UserRepository _userRepository;
        private readonly CustomerRepository _customerRepository;

        public ReportForm(AppointmentRepository appointmentRepository, UserRepository userRepository, CustomerRepository customerRepository)
        {
            InitializeComponent();
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }

        private void btnMonthly_Click(object sender, EventArgs e)
        {
            var MonthlyForm = new MonthlyReportForm(_appointmentRepository);
            MonthlyForm.Show();
        }

        private void btnReport2_Click(object sender, EventArgs e)
        {
            var report2Form = new ConsultantScheduleForm(_userRepository,_appointmentRepository);
            report2Form.Show();
        }

        private void btnReport3_Click(object sender, EventArgs e)
        {
            var report3Form = new CustomerActivityReportForm(_customerRepository, _appointmentRepository);
            report3Form.Show();
        }
    }
}
