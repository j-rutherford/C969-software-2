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
            txtAddressLine1.Text = _customer.AddressLine1;
            txtAddressLine2.Text = _customer.AddressLine2;
            txtPostalCode.Text = _customer.PostalCode;
            txtPhone.Text = _customer.Phone;
            txtCity.Text = _customer.CityName;
            txtCountry.Text = _customer.CountryName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _customer.CustomerName = txtCustomerName.Text;
            _customer.AddressLine1 = txtAddressLine1.Text;
            _customer.AddressLine2 = txtAddressLine2.Text;
            _customer.PostalCode = txtPostalCode.Text;
            _customer.Phone = txtPhone.Text;
            _customer.CityName = txtCity.Text;
            _customer.CountryName = txtCountry.Text;
            _customer.Active = true;  // Or whatever logic you need

            if (_customer.CustomerId == 0)  // New customer
            {
                _customer.CreateDate = DateTime.UtcNow;
                _customer.CreatedBy = "YourUserName";  // Replace with actual user
                _customer.LastUpdate = DateTime.UtcNow;
                _customer.LastUpdateBy = "YourUserName";  // Replace with actual user

                _customerRepository.AddCustomer(_customer);
            }
            else  // Existing customer
            {
                _customer.LastUpdate = DateTime.UtcNow;
                _customer.LastUpdateBy = "YourUserName";  // Replace with actual user

                _customerRepository.UpdateCustomer(_customer);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}