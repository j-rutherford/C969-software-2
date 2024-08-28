using C969App.Data;
using C969App.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace C969App.Forms
{
    public partial class AppointmentForm : Form
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly UserRepository _userRepository;

        public AppointmentForm(AppointmentRepository appointmentRepository, CustomerRepository customerRepository, UserRepository userRepository)
        {
            InitializeComponent();
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            dgvAppointments.CellFormatting += dgvAppointments_CellFormatting;
            LoadAppointments();
        }
        private void LoadAppointments()
        {
            try
            {
                var appointments = _appointmentRepository.GetAllAppointments();

                // Convert StartTime and EndTime from UTC to local time
                foreach (DataRow row in appointments.Rows)
                {
                    if (DateTime.TryParse(row["AppointmentDate"].ToString(), out DateTime appointmentDate) &&
                        DateTime.TryParse(row["StartTime"].ToString(), out DateTime startTimeUtc) &&
                        DateTime.TryParse(row["EndTime"].ToString(), out DateTime endTimeUtc))
                    {
                        // Combine date and time for conversion
                        DateTime startDateTimeUtc = appointmentDate.Date + startTimeUtc.TimeOfDay;
                        DateTime endDateTimeUtc = appointmentDate.Date + endTimeUtc.TimeOfDay;

                        // Convert to local time
                        var localStartTime = TimeZoneInfo.ConvertTimeFromUtc(startDateTimeUtc, TimeZoneInfo.Local);
                        var localEndTime = TimeZoneInfo.ConvertTimeFromUtc(endDateTimeUtc, TimeZoneInfo.Local);

                        // Replace the original time values with the converted local times
                        row["StartTime"] = localStartTime.ToString("HH:mm");
                        row["EndTime"] = localEndTime.ToString("HH:mm");

                        // Keep the original appointment date as it is
                        row["AppointmentDate"] = appointmentDate.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        // Handle cases where the date parsing fails
                        MessageBox.Show($"Error parsing date for appointment ID {row["AppointmentId"]}", "Date Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                dgvAppointments.DataSource = appointments;

                // Set the visibility and headers of the columns

                dgvAppointments.Columns["AppointmentId"].Visible = false;
                dgvAppointments.Columns["CustomerId"].Visible = false;
                dgvAppointments.Columns["UserId"].Visible = false;

                dgvAppointments.Columns["CustomerName"].HeaderText = "Customer";
                dgvAppointments.Columns["UserName"].HeaderText = "Consultant";
                dgvAppointments.Columns["Type"].HeaderText = "Appointment Type";
                dgvAppointments.Columns["AppointmentDate"].HeaderText = "Date";
                dgvAppointments.Columns["StartTime"].HeaderText = "Start Time";
                dgvAppointments.Columns["EndTime"].HeaderText = "End Time";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            var appointmentDetailForm = new AppointmentDetailForm(_appointmentRepository, _customerRepository, _userRepository);
            appointmentDetailForm.ShowDialog();
            LoadAppointments(); // Reload the appointments after adding a new one
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count > 0)
            {
                int appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentId"].Value);
                var appointment = _appointmentRepository.GetAppointmentById(appointmentId);

                if (appointment != null)
                {
                    var appointmentDetailForm = new AppointmentDetailForm(_appointmentRepository, _customerRepository, _userRepository, appointment);
                    appointmentDetailForm.ShowDialog();
                    LoadAppointments(); // Reload the appointments after editing
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count > 0)
            {
                int appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentId"].Value);

                var confirmation = MessageBox.Show("Are you sure you want to delete this appointment?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmation == DialogResult.Yes)
                {
                    _appointmentRepository.DeleteAppointment(appointmentId);
                    LoadAppointments(); // Reload the appointments after deletion
                }
            }
        }
        private void dgvAppointments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvAppointments.Columns[e.ColumnIndex].Name == "Start" || dgvAppointments.Columns[e.ColumnIndex].Name == "End")
            {
                if (e.Value != null && e.Value is DateTime dateTime)
                {
                    // Display the DateTime in a specific format, e.g., "yyyy-MM-dd HH:mm"
                    e.Value = dateTime.ToString("yyyy-MM-dd HH:mm");
                    e.FormattingApplied = true;
                }
            }
        }
        private void btnAllAppointments_Click(object sender, EventArgs e)
        {
            LoadAppointments();
        }

        private void btnCurrentWeek_Click(object sender, EventArgs e)
        {
            LoadAppointments(DateTime.Now.StartOfWeek(DayOfWeek.Monday), DateTime.Now.EndOfWeek(DayOfWeek.Sunday));
        }

        private void btnCurrentMonth_Click(object sender, EventArgs e)
        {
            LoadAppointments(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                             new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)));
        }
        private void LoadAppointments(DateTime? startDate = null, DateTime? endDate = null)
        {
            var appointments = _appointmentRepository.GetAllAppointments();

            if (startDate.HasValue && endDate.HasValue)
            {
                //Lamda required for linq query, creates row as an on-the-fly parameter
                var filteredAppointments = appointments.AsEnumerable()
                    .Where(row => row.Field<DateTime>("AppointmentDate") >= startDate.Value && row.Field<DateTime>("AppointmentDate") <= endDate.Value);

                if (filteredAppointments.Any())
                {
                    appointments = filteredAppointments.CopyToDataTable();
                }
                else
                {
                    MessageBox.Show("No appointments found for the selected date range.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvAppointments.DataSource = null; // Clear the DataGridView
                    return;
                }
            }

            dgvAppointments.DataSource = appointments;

            // Set the visibility and headers of the columns
            dgvAppointments.Columns["AppointmentId"].Visible = false;
            dgvAppointments.Columns["CustomerName"].HeaderText = "Customer";
            dgvAppointments.Columns["UserName"].HeaderText = "Consultant";
            dgvAppointments.Columns["Type"].HeaderText = "Appointment Type";
            dgvAppointments.Columns["AppointmentDate"].HeaderText = "Date";
            dgvAppointments.Columns["StartTime"].HeaderText = "Start Time";
            dgvAppointments.Columns["EndTime"].HeaderText = "End Time";
        }

    }
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek endOfWeek)
        {
            return dt.StartOfWeek(endOfWeek).AddDays(6);
        }
    }

}
