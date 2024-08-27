using System;
using System.Windows.Forms;
using C969App.Data;
using C969App.Models;
using System.Collections.Generic;

namespace C969App.Forms
{
    public partial class AppointmentDetailForm : Form
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly UserRepository _userRepository;
        private Appointment _appointment;

        // Constructor for adding a new appointment
        public AppointmentDetailForm(AppointmentRepository appointmentRepository, CustomerRepository customerRepository, UserRepository userRepository)
        {
            InitializeComponent();
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _appointment = new Appointment();
            LoadComboBoxes();
        }

        // Constructor for editing an existing appointment
        public AppointmentDetailForm(AppointmentRepository appointmentRepository, CustomerRepository customerRepository, UserRepository userRepository, Appointment appointment) : this(appointmentRepository, customerRepository, userRepository)
        {
            _appointment = appointment;
            PopulateFields();
        }

        private void LoadComboBoxes()
        {
            // Load Customers into ComboBox
            var customers = _customerRepository.GetAllCustomers();
            cmbCustomer.DataSource = customers;
            cmbCustomer.DisplayMember = "CustomerName";
            cmbCustomer.ValueMember = "CustomerId";

            // Load Users into ComboBox
            var users = _userRepository.GetAllUsers();
            cmbUser.DataSource = users;
            cmbUser.DisplayMember = "UserName";
            cmbUser.ValueMember = "UserId";
        }
        private void PopulateFields()
        {
            // Ensure that the CustomerId and UserId match the values in the combo boxes
            if (cmbCustomer.Items.Count > 0)
            {
                cmbCustomer.SelectedValue = _appointment.CustomerId;
            }
            if (cmbUser.Items.Count > 0)
            {
                cmbUser.SelectedValue = _appointment.UserId;
            }

            txtTitle.Text = _appointment.Title ?? string.Empty;
            txtDescription.Text = _appointment.Description ?? string.Empty;
            txtLocation.Text = _appointment.Location ?? string.Empty;
            txtContact.Text = _appointment.Contact ?? string.Empty;
            txtType.Text = _appointment.Type ?? string.Empty;

            // Validate and set Start DateTime
            if (_appointment.Start > dtpStart.MinDate && _appointment.Start < dtpStart.MaxDate)
            {
                dtpStart.Value = _appointment.Start.ToLocalTime();
            }
            else
            {
                dtpStart.Value = DateTime.Now; // Set to a default value within a valid range
            }

            // Validate and set End DateTime
            if (_appointment.End > dtpEnd.MinDate && _appointment.End < dtpEnd.MaxDate)
            {
                dtpEnd.Value = _appointment.End.ToLocalTime();
            }
            else
            {
                dtpEnd.Value = DateTime.Now.AddHours(1); // Set to a default value within a valid range
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            _appointment.CustomerId = (int)cmbCustomer.SelectedValue;
            _appointment.UserId = (int)cmbUser.SelectedValue;
            _appointment.Title = txtTitle.Text;
            _appointment.Description = txtDescription.Text;
            _appointment.Location = txtLocation.Text;
            _appointment.Contact = txtContact.Text;
            _appointment.Type = txtType.Text;
            _appointment.Start = dtpStart.Value.ToUniversalTime();
            _appointment.End = dtpEnd.Value.ToUniversalTime();
            _appointment.LastUpdate = DateTime.UtcNow;
            _appointment.LastUpdateBy = "YourUserName";  // Replace with actual user

            if (_appointment.AppointmentId == 0)  // New appointment
            {
                _appointment.CreateDate = DateTime.UtcNow;
                _appointment.CreatedBy = "YourUserName";  // Replace with actual user
                _appointmentRepository.AddAppointment(_appointment);
            }
            else  // Existing appointment
            {
                _appointmentRepository.UpdateAppointment(_appointment);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
