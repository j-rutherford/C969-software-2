using C969App.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969App.Forms
{
    public partial class ConsultantScheduleForm : Form
    {
        private readonly UserRepository _userRepository;
        private readonly AppointmentRepository _appointmentRepository;

        public ConsultantScheduleForm(UserRepository userRepository, AppointmentRepository appointmentRepository)
        {
            InitializeComponent();
            _userRepository = userRepository;
            _appointmentRepository = appointmentRepository;

            PopulateConsultantComboBox();
        }

        private void PopulateConsultantComboBox()
        {
            var users = _userRepository.GetAllUsers();
            cmbConsultants.DataSource = users;
            cmbConsultants.DisplayMember = "UserName";
            cmbConsultants.ValueMember = "UserId";
        }

        private void btnViewSchedule_Click(object sender, EventArgs e)
        {
            var selectedUserId = (int)cmbConsultants.SelectedValue;
            var schedule = _appointmentRepository.GetAppointmentsByUser(selectedUserId);

            dgvSchedule.DataSource = schedule;
        }

    }
}
