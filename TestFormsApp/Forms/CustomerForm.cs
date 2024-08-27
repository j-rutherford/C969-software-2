using System;
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

            // Set the visible columns
            dgvCustomers.Columns["CustomerId"].Visible = true;
            dgvCustomers.Columns["CustomerName"].Visible = true;
            dgvCustomers.Columns["AddressLine1"].Visible = true;
            dgvCustomers.Columns["AddressLine2"].Visible = true;
            dgvCustomers.Columns["CityName"].Visible = true;
            dgvCustomers.Columns["CountryName"].Visible = true;
            dgvCustomers.Columns["PostalCode"].Visible = true;
            dgvCustomers.Columns["Phone"].Visible = true;


        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                // Create a new Customer object with the input values from the form
                var newCustomer = new Customer
                {
                    CustomerName = txtCustomerName.Text,
                    AddressLine1 = txtAddressLine1.Text,
                    AddressLine2 = txtAddressLine2.Text,
                    PostalCode = txtPostalCode.Text,
                    Phone = txtPhone.Text,
                    CityName = txtCity.Text,
                    CountryName = txtCountry.Text,
                    Active = true, // Set this based on your logic
                    CreateDate = DateTime.UtcNow, // Assuming all times are stored in UTC
                    CreatedBy = "YourUserName", // Replace with the current user's name
                    LastUpdate = DateTime.UtcNow,
                    LastUpdateBy = "YourUserName" // Replace with the current user's name
                };

                // Add the new customer to the database
                _customerRepository.AddCustomer(newCustomer);

                // Refresh the DataGridView and clear the form fields
                LoadCustomers();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please fill in all required fields.", "Add Customer Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0 && ValidateFields())
            {
                var customer = new Customer
                {
                    CustomerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["CustomerId"].Value),
                    CustomerName = txtCustomerName.Text,
                    AddressId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["AddressId"].Value),
                    AddressLine1 = txtAddressLine1.Text,
                    AddressLine2 = txtAddressLine2.Text,
                    PostalCode = txtPostalCode.Text,
                    Phone = txtPhone.Text,
                    CityId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["CityId"].Value),
                    CityName = txtCity.Text,
                    CountryId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["CountryId"].Value),
                    CountryName = txtCountry.Text,
                    Active = true // or based on logic
                };

                _customerRepository.UpdateCustomer(customer);

                LoadCustomers();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please select a customer to update.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                var customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["CustomerId"].Value);
                _customerRepository.DeleteCustomer(customerId);
                LoadCustomers();
                ClearFields();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                txtCustomerName.Text = dgvCustomers.SelectedRows[0].Cells["CustomerName"].Value.ToString();
                txtAddressLine1.Text = dgvCustomers.SelectedRows[0].Cells["AddressLine1"].Value.ToString();
                txtAddressLine2.Text = dgvCustomers.SelectedRows[0].Cells["AddressLine2"].Value.ToString();
                txtPostalCode.Text = dgvCustomers.SelectedRows[0].Cells["PostalCode"].Value.ToString();
                txtPhone.Text = dgvCustomers.SelectedRows[0].Cells["Phone"].Value.ToString();
                txtCity.Text = dgvCustomers.SelectedRows[0].Cells["City"].Value.ToString();
                txtCountry.Text = dgvCustomers.SelectedRows[0].Cells["Country"].Value.ToString();
            }
        }

        private bool ValidateFields()
        {
            return true;
        }

        private void ClearFields()
        {
            txtCustomerName.Clear();
            txtAddressLine1.Clear();
            txtAddressLine2.Clear();
            txtPostalCode.Clear();
            txtPhone.Clear();
            txtCity.Clear();
            txtCountry.Clear();
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
