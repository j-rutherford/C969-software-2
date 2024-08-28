using System;
using System.Linq;
using System.Windows.Forms;
using C969App.Data;
using C969App.Models;

namespace C969App.Forms
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerForm(CustomerRepository customerRepository)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();
            dgvCustomers.DataSource = customers;

            foreach (DataGridViewColumn column in dgvCustomers.Columns)
            {
                column.Visible = false;
            }
            // Set the visible columns
            dgvCustomers.Columns["CustomerId"].Visible = true;
            dgvCustomers.Columns["CustomerName"].Visible = true;
            dgvCustomers.Columns["Address"].Visible = true;
            dgvCustomers.Columns["CityName"].Visible = true;
            dgvCustomers.Columns["CountryName"].Visible = true;
            dgvCustomers.Columns["PostalCode"].Visible = true;
            dgvCustomers.Columns["Phone"].Visible = true;
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            var customerDetailForm = new CustomerDetailForm(_customerRepository);
            customerDetailForm.ShowDialog();
            LoadCustomers();  // Refresh the customer list after adding
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                var customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["CustomerId"].Value);
                var customer = _customerRepository.GetCustomerById(customerId);

                var customerDetailForm = new CustomerDetailForm(_customerRepository, customer);
                customerDetailForm.ShowDialog();
                LoadCustomers();  // Refresh the customer list after editing
            }
            else
            {
                MessageBox.Show("Please select a customer to edit.", "Edit Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCustomers.SelectedRows.Count > 0)
                {
                    var customerName = dgvCustomers.SelectedRows[0].Cells["CustomerName"].Value.ToString();
                    var confirmResult = MessageBox.Show(
                        $"Are you sure you want to delete the customer '{customerName}'?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        var customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["CustomerId"].Value);
                        _customerRepository.DeleteCustomer(customerId);
                        LoadCustomers();  // Refresh the customer list after deletion
                    }
                }
                else
                {
                    MessageBox.Show("Please select a customer to delete.", "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the customer record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }
    }
}
