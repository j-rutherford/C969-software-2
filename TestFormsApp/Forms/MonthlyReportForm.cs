using System;
using System.Data;
using System.Windows.Forms;
using C969App.Data;

namespace C969App.Forms
{
    public partial class MonthlyReportForm : Form
    {
        private readonly AppointmentRepository _appointmentRepository;

        public MonthlyReportForm(AppointmentRepository appointmentRepository)
        {
            InitializeComponent();
            _appointmentRepository = appointmentRepository;
        }

        private void MonthlyForm_Load(object sender, EventArgs e)
        {
            LoadMonthlyReport();
        }

        private void LoadMonthlyReport()
        {
            try
            {
                var reportData = _appointmentRepository.GetMonthlyAppointmentReport();
                dgvMonthlyReport.DataSource = reportData;

                dgvMonthlyReport.Columns["Month"].HeaderText = "Month";
                dgvMonthlyReport.Columns["Type"].HeaderText = "Appointment Type";
                dgvMonthlyReport.Columns["Count"].HeaderText = "Number of Appointments";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
