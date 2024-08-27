using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using C969App.Models;
using System.Linq;
using System.Windows.Forms;

namespace C969App.Data
{
    public class CustomerRepository
    {
        private readonly DbContext _context;

        public CustomerRepository(DbContext context)
        {
            _context = context;
        }

        // Retrieve all customers with associated country, city, and address
        public List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();

            string query = @"SELECT c.CustomerId, c.CustomerName, 
                            a.AddressId, a.Address, a.PostalCode, a.Phone, 
                            ct.CityId, ct.City AS CityName, 
                            co.CountryId, co.Country AS CountryName
                     FROM customer c
                     INNER JOIN address a ON c.AddressId = a.AddressId
                     INNER JOIN city ct ON a.CityId = ct.CityId
                     INNER JOIN country co ON ct.CountryId = co.CountryId";

            using (var command = new MySqlCommand(query, _context.Connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var customer = new Customer
                    {
                        CustomerId = reader.GetInt32("CustomerId"),
                        CustomerName = reader.GetString("CustomerName"),
                        AddressId = reader.GetInt32("AddressId"),
                        Address = reader.GetString("Address"),
                        PostalCode = reader.GetString("PostalCode"),
                        Phone = reader.GetString("Phone"),
                        CityId = reader.GetInt32("CityId"),
                        CityName = reader.GetString("CityName"),
                        CountryId = reader.GetInt32("CountryId"),
                        CountryName = reader.GetString("CountryName")
                    };

                    customers.Add(customer);
                }
            }

            return customers;
        }

        public Customer GetCustomerById(int id)
        {
            return GetAllCustomers().FirstOrDefault(x => x.CustomerId == id);
        }
        // Add a new customer and associated country, city, and address
        public void AddCustomer(Customer customer)
        {
            using (var transaction = _context.Connection.BeginTransaction())
            {
                try
                {
                    // Insert into country
                    string countryQuery = @"INSERT INTO country (Country, createDate, createdBy, lastUpdate, lastUpdateBy)
                                    VALUES (@CountryName, NOW(), 'ADMIN', NOW(), 'ADMIN')";
                    using (var command = new MySqlCommand(countryQuery, _context.Connection, transaction))
                    {
                        command.Parameters.AddWithValue("@CountryName", customer.CountryName);
                        command.ExecuteNonQuery();
                        customer.CountryId = (int)command.LastInsertedId;
                    }

                    // Insert into city
                    string cityQuery = @"INSERT INTO city (City, CountryId, createDate, createdBy, lastUpdate, lastUpdateBy)
                                 VALUES (@CityName, @CountryId, NOW(), 'ADMIN', NOW(), 'ADMIN')";
                    using (var command = new MySqlCommand(cityQuery, _context.Connection, transaction))
                    {
                        command.Parameters.AddWithValue("@CityName", customer.CityName);
                        command.Parameters.AddWithValue("@CountryId", customer.CountryId);
                        command.ExecuteNonQuery();
                        customer.CityId = (int)command.LastInsertedId;
                    }

                    // Insert into address
                    string addressQuery = @"INSERT INTO address (Address, address2, PostalCode, Phone, CityId, createDate, createdBy, lastUpdate, lastUpdateBy)
                                    VALUES (@Address,'not required', @PostalCode, @Phone, @CityId, NOW(), 'ADMIN', NOW(), 'ADMIN')";
                    using (var command = new MySqlCommand(addressQuery, _context.Connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Address", customer.Address);
                        command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                        command.Parameters.AddWithValue("@Phone", customer.Phone);
                        command.Parameters.AddWithValue("@CityId", customer.CityId);
                        command.ExecuteNonQuery();
                        customer.AddressId = (int)command.LastInsertedId;
                    }

                    // Insert into customer
                    string customerQuery = @"INSERT INTO customer (CustomerName, AddressId, Active, CreateDate, CreatedBy, LastUpdate, LastUpdateBy)
                                     VALUES (@CustomerName, @AddressId, 1, NOW(), 'ADMIN', NOW(), 'ADMIN')";
                    using (var command = new MySqlCommand(customerQuery, _context.Connection, transaction))
                    {
                        command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                        command.Parameters.AddWithValue("@AddressId", customer.AddressId);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("ERROR: \n" + ex.ToString());
                }
            }
        }


        // Update an existing customer and associated country, city, and address
        public void UpdateCustomer(Customer customer)
        {
            using (var transaction = _context.Connection.BeginTransaction())
            {
                try
                {
                    // Update the country
                    string countryQuery = @"UPDATE country 
                                    SET Country = @CountryName
                                    WHERE CountryId = @CountryId";
                    using (var command = new MySqlCommand(countryQuery, _context.Connection, transaction))
                    {
                        command.Parameters.AddWithValue("@CountryName", customer.CountryName);
                        command.Parameters.AddWithValue("@CountryId", customer.CountryId);
                        command.ExecuteNonQuery();
                    }

                    // Update the city
                    string cityQuery = @"UPDATE city 
                                 SET City = @CityName
                                 WHERE CityId = @CityId";
                    using (var command = new MySqlCommand(cityQuery, _context.Connection, transaction))
                    {
                        command.Parameters.AddWithValue("@CityName", customer.CityName);
                        command.Parameters.AddWithValue("@CityId", customer.CityId);
                        command.ExecuteNonQuery();
                    }

                    // Update the address
                    string addressQuery = @"UPDATE address 
                                    SET Address = @Address, 
                                        PostalCode = @PostalCode, 
                                        Phone = @Phone
                                    WHERE AddressId = @AddressId";
                    using (var command = new MySqlCommand(addressQuery, _context.Connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Address", customer.Address);
                        command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                        command.Parameters.AddWithValue("@Phone", customer.Phone);
                        command.Parameters.AddWithValue("@AddressId", customer.AddressId);
                        command.ExecuteNonQuery();
                    }

                    // Update the customer
                    string customerQuery = @"UPDATE customer 
                                     SET CustomerName = @CustomerName, 
                                         AddressId = @AddressId
                                     WHERE CustomerId = @CustomerId";
                    using (var command = new MySqlCommand(customerQuery, _context.Connection, transaction))
                    {
                        command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                        command.Parameters.AddWithValue("@AddressId", customer.AddressId);
                        command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                        command.ExecuteNonQuery();
                    }

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("ERROR: \n" + ex.ToString());
                }
            }
        }

        // Delete a customer and associated address, city, and country
        public void DeleteCustomer(int customerId)
        {
            string query = @"DELETE FROM customer WHERE CustomerId = @CustomerId;

                             DELETE FROM address WHERE AddressId = (SELECT AddressId FROM customer WHERE CustomerId = @CustomerId);

                             DELETE FROM city WHERE CityId = (SELECT CityId FROM address WHERE AddressId = (SELECT AddressId FROM customer WHERE CustomerId = @CustomerId));

                             DELETE FROM country WHERE CountryId = (SELECT CountryId FROM city WHERE CityId = (SELECT CityId FROM address WHERE AddressId = (SELECT AddressId FROM customer WHERE CustomerId = @CustomerId)));";

            try
            {
                using (var command = new MySqlCommand(query, _context.Connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
