﻿using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using C969App.Data;
using C969App.Models;
using MySql.Data.MySqlClient;

namespace C969App.Forms
{
    public partial class AppointmentForm : Form
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly UserRepository _userRepository;

        public AppointmentForm(
            AppointmentRepository appointmentRepository,
            CustomerRepository customerRepository,
            UserRepository userRepository)
        {
            InitializeComponent();
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;

            this.Load += AppointmentForm_Load;
        }
        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            LoadAppointments(); // Populate the DataGridView when the form loads
        }
        private void LoadAppointments()
        {
            var dataTable = _appointmentRepository.GetAllAppointments();
            dgvAppointments.DataSource = dataTable;

            // Set the visible columns
            dgvAppointments.Columns["AppointmentId"].Visible = true;
            dgvAppointments.Columns["CustomerName"].Visible = true;
            dgvAppointments.Columns["UserName"].Visible = true;
            dgvAppointments.Columns["Title"].Visible = true;
            dgvAppointments.Columns["Location"].Visible = true;
            dgvAppointments.Columns["Contact"].Visible = true;
            dgvAppointments.Columns["Type"].Visible = true;
            dgvAppointments.Columns["Start"].Visible = true;
            dgvAppointments.Columns["End"].Visible = true;

            // Optionally, format the date and time columns
            dgvAppointments.Columns["Start"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dgvAppointments.Columns["End"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";


            foreach (DataGridViewColumn column in dgvAppointments.Columns)
            {
                column.Visible = false;
            }


            // Set the visible columns
            dgvAppointments.Columns["AppointmentId"].Visible = true;
            dgvAppointments.Columns["CustomerName"].Visible = true;
            dgvAppointments.Columns["UserName"].Visible = true;
            dgvAppointments.Columns["Title"].Visible = true;
            dgvAppointments.Columns["Location"].Visible = true;
            dgvAppointments.Columns["Contact"].Visible = true;
            dgvAppointments.Columns["Type"].Visible = true;
            dgvAppointments.Columns["Start"].Visible = true;
            dgvAppointments.Columns["End"].Visible = true;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var appointmentDetailForm = new AppointmentDetailForm(_appointmentRepository, _customerRepository, _userRepository);
            appointmentDetailForm.ShowDialog();

            LoadAppointments(); // Refresh the appointment list after adding a new appointment
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count > 0)
            {
                var appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentId"].Value);
                var appointment = _appointmentRepository.GetAppointmentById(appointmentId);

                if (appointment != null)
                {
                    var appointmentDetailForm = new AppointmentDetailForm(_appointmentRepository, _customerRepository, _userRepository, appointment);
                    appointmentDetailForm.ShowDialog();

                    LoadAppointments(); // Refresh the appointment list after editing an appointment
                }
                else
                {
                    MessageBox.Show("Appointment not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count > 0)
            {
                var appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentId"].Value);

                var confirmResult = MessageBox.Show(
                    "Are you sure you want to delete this appointment?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    _appointmentRepository.DeleteAppointment(appointmentId);
                    LoadAppointments();  // Refresh the list after deletion
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to delete.", "Delete Appointment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAppointments();  // Refresh the appointment list manually
        }
    }
}
