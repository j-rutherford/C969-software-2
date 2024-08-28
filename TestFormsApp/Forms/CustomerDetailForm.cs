using C969App.Data;
using System.Windows.Forms;
using System;

namespace C969App.Forms
{
    public partial class CustomerDetailForm : Form
    {
        private readonly CustomerRepository _customerRepository;
        private Customer _customer;

        // Constructor for adding a new customer
        public CustomerDetailForm(CustomerRepository customerRepository)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
            _customer = new Customer();
        }

        // Constructor for editing an existing customer
        public CustomerDetailForm(CustomerRepository customerRepository, Customer customer) : this(customerRepository)
        {
            _customer = customer;
            PopulateFields();
        }

        private void PopulateFields()
        {
            txtCustomerName.Text = _customer.CustomerName;
            txtAddress.Text = _customer.Address;
            txtPostalCode.Text = _customer.PostalCode;
            txtPhone.Text = _customer.Phone;
            txtCity.Text = _customer.CityName;
            txtCountry.Text = _customer.CountryName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateCustomerFields())
                {
                    _customer.CustomerName = txtCustomerName.Text;
                    _customer.Address = txtAddress.Text;
                    _customer.PostalCode = txtPostalCode.Text;
                    _customer.Phone = txtPhone.Text;
                    _customer.CityName = txtCity.Text;
                    _customer.CountryName = txtCountry.Text;

                    if (_customer.CustomerId == 0)  // New customer
                    {
                        _customerRepository.AddCustomer(_customer);
                    }
                    else  // Existing customer
                    {
                        _customerRepository.UpdateCustomer(_customer);
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the customer record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateCustomerFields()
        {
            // Trim the text fields
            txtCustomerName.Text = txtCustomerName.Text.Trim();
            txtAddress.Text = txtAddress.Text.Trim();
            txtCity.Text = txtCity.Text.Trim();
            txtPostalCode.Text = txtPostalCode.Text.Trim();
            txtCountry.Text = txtCountry.Text.Trim();
            txtPhone.Text = txtPhone.Text.Trim();

            // Check if fields are non-empty
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtPostalCode.Text) ||
                string.IsNullOrWhiteSpace(txtCountry.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate the phone number
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, @"^[\d-]+$"))
            {
                MessageBox.Show("Phone number can only contain digits and dashes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

    }
}