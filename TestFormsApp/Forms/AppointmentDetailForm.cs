using System;
using System.Windows.Forms;
using C969App.Data;
using C969App.Models;


namespace C969App.Forms
{
    public partial class AppointmentDetailForm : Form
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly UserRepository _userRepository;
        private AppointmentDTO _appointment;

        // Constructor for adding a new appointment
        public AppointmentDetailForm(AppointmentRepository appointmentRepository, CustomerRepository customerRepository, UserRepository userRepository)
        {
            InitializeComponent();
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _appointment = new AppointmentDTO();

            PopulateCustomerComboBox();
            PopulateUserComboBox();
        }

        // Constructor for editing an existing appointment
        public AppointmentDetailForm(AppointmentRepository appointmentRepository, CustomerRepository customerRepository, UserRepository userRepository, AppointmentDTO appointment)
            : this(appointmentRepository, customerRepository, userRepository)
        {
            _appointment = appointment;
            PopulateFields();
        }

        private void PopulateCustomerComboBox()
        {
            var customers = _customerRepository.GetAllCustomers();
            cmbCustomer.DataSource = customers;
            cmbCustomer.DisplayMember = "CustomerName";
            cmbCustomer.ValueMember = "CustomerId";
        }

        private void PopulateUserComboBox()
        {
            var users = _userRepository.GetAllUsers();
            cmbUser.DataSource = users;
            cmbUser.DisplayMember = "UserName";
            cmbUser.ValueMember = "UserId";
        }

        private void PopulateFields()
        {
            // Populate the combo boxes first
            PopulateCustomerComboBox();
            PopulateUserComboBox();

            // Set the values
            txtType.Text = _appointment.Type;
            dtpDate.Value = _appointment.Start.Date;
            dtpStartTime.Value = _appointment.Start;
            dtpEndTime.Value = _appointment.End;

            // Ensure the combo boxes have the correct ValueMember and DisplayMember set
            cmbCustomer.SelectedValue = _appointment.CustomerId;
            cmbUser.SelectedValue = _appointment.UserId;

            // Check if the SelectedValue was set correctly
            if (cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Customer not found in the list.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cmbUser.SelectedValue == null)
            {
                MessageBox.Show("User not found in the list.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            _appointment.Type = txtType.Text.Trim();
            _appointment.CustomerId = (int)cmbCustomer.SelectedValue;
            _appointment.UserId = (int)cmbUser.SelectedValue;

            // Combine the selected date with the times
            var start = dtpDate.Value.Date + dtpStartTime.Value.TimeOfDay;
            var end = dtpDate.Value.Date + dtpEndTime.Value.TimeOfDay;

            _appointment.Start = TimeZoneInfo.ConvertTimeToUtc(start, TimeZoneInfo.Local);
            _appointment.End = TimeZoneInfo.ConvertTimeToUtc(end, TimeZoneInfo.Local);
            if (_appointment.AppointmentId == 0)  // New appointment
            {
                _appointmentRepository.AddAppointment(_appointment);
            }
            else  // Existing appointment
            {
                _appointmentRepository.UpdateAppointment(_appointment);
            }

            this.Close();
        }

        private bool ValidateFields()
        {
            // Basic validation checks
            if (string.IsNullOrWhiteSpace(txtType.Text) ||
                cmbCustomer.SelectedValue == null ||
                cmbUser.SelectedValue == null)
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (dtpEndTime.Value.TimeOfDay <= dtpStartTime.Value.TimeOfDay)
            {
                MessageBox.Show("End time must be after start time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Combine date with the start and end times
            var start = dtpDate.Value.Date + dtpStartTime.Value.TimeOfDay;
            var end = dtpDate.Value.Date + dtpEndTime.Value.TimeOfDay;

            // Convert 9 AM and 5 PM EST to UTC
            var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var est9amUtc = TimeZoneInfo.ConvertTimeToUtc(new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, 9, 0, 0), estTimeZone);
            var est5pmUtc = TimeZoneInfo.ConvertTimeToUtc(new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, 17, 0, 0), estTimeZone);

            // Convert 9 AM and 5 PM EST from UTC to the user's local time
            var local9am = TimeZoneInfo.ConvertTimeFromUtc(est9amUtc, TimeZoneInfo.Local);
            var local5pm = TimeZoneInfo.ConvertTimeFromUtc(est5pmUtc, TimeZoneInfo.Local);

            // Convert the start and end times to UTC
            var startUtc = TimeZoneInfo.ConvertTimeToUtc(start, TimeZoneInfo.Local);
            var endUtc = TimeZoneInfo.ConvertTimeToUtc(end, TimeZoneInfo.Local);

            // Check if the times are within the business hours
            if (startUtc < est9amUtc || endUtc > est5pmUtc)
            {
                MessageBox.Show($"Appointments must be scheduled between 09:00 and 17:00 EST, which is between {local9am:HH:mm} and {local5pm:HH:mm} in your local time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Check for overlapping appointments by User/Consultant
            if (IsOverlappingAppointment((int)cmbUser.SelectedValue, startUtc, endUtc, _appointment.AppointmentId))
            {
                MessageBox.Show("The selected appointment time overlaps with another appointment for this consultant.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Check for weekend appointments
            if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("Appointments can only be scheduled Monday through Friday.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool IsOverlappingAppointment(int userId, DateTime start, DateTime end, int? excludeAppointmentId = null)
        {
            var appointments = _appointmentRepository.GetAppointmentsByUser(userId);

            foreach (var appointment in appointments)
            {
                // Skip the current appointment being edited
                if (excludeAppointmentId.HasValue && appointment.AppointmentId == excludeAppointmentId.Value)
                    continue;

                // Check if the time slots overlap
                if ((start < appointment.End && end > appointment.Start) ||
                    (start >= appointment.Start && start < appointment.End) ||
                    (end > appointment.Start && end <= appointment.End))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
