using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using C969App.Models;

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
                            a.AddressId, a.Address AS AddressLine1, a.Address2 AS AddressLine2, a.PostalCode, a.Phone, 
                            ct.CityId, ct.City AS CityName, 
                            co.CountryId, co.Country AS CountryName, 
                            c.Active, c.CreateDate, c.CreatedBy, c.LastUpdate, c.LastUpdateBy
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
                        AddressLine1 = reader.GetString("AddressLine1"),
                        AddressLine2 = reader.GetString("AddressLine2"),
                        PostalCode = reader.GetString("PostalCode"),
                        Phone = reader.GetString("Phone"),
                        CityId = reader.GetInt32("CityId"),
                        CityName = reader.GetString("CityName"),
                        CountryId = reader.GetInt32("CountryId"),
                        CountryName = reader.GetString("CountryName"),
                        Active = reader.GetBoolean("Active"),
                        CreateDate = reader.GetDateTime("CreateDate"),
                        CreatedBy = reader.GetString("CreatedBy"),
                        LastUpdate = reader.GetDateTime("LastUpdate"),
                        LastUpdateBy = reader.GetString("LastUpdateBy")
                    };

                    customers.Add(customer);
                }
            }

            return customers;
        }

        // Add a new customer and associated country, city, and address
        public void AddCustomer(Customer customer)
        {
            string query = @"INSERT INTO country (Country, createDate, createdBy, lastUpdate, lastUpdateBy)
                     VALUES (@CountryName, @CreateDate, @CreatedBy, @LastUpdate, @LastUpdateBy)
                     ON DUPLICATE KEY UPDATE CountryId=LAST_INSERT_ID(CountryId);

                     INSERT INTO city (City, CountryId, createDate, createdBy, lastUpdate, lastUpdateBy)
                     VALUES (@CityName, LAST_INSERT_ID(), @CreateDate, @CreatedBy, @LastUpdate, @LastUpdateBy)
                     ON DUPLICATE KEY UPDATE CityId=LAST_INSERT_ID(CityId);

                     INSERT INTO address (Address, Address2, PostalCode, Phone, CityId, createDate, createdBy, lastUpdate, lastUpdateBy)
                     VALUES (@AddressLine1, @AddressLine2, @PostalCode, @Phone, LAST_INSERT_ID(), @CreateDate, @CreatedBy, @LastUpdate, @LastUpdateBy);

                     INSERT INTO customer (CustomerName, AddressId, Active, CreateDate, CreatedBy, LastUpdate, LastUpdateBy)
                     VALUES (@CustomerName, LAST_INSERT_ID(), @Active, @CreateDate, @CreatedBy, @LastUpdate, @LastUpdateBy);";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                var currentDate = DateTime.UtcNow;
                command.Parameters.AddWithValue("@CountryName", customer.CountryName);
                command.Parameters.AddWithValue("@CityName", customer.CityName);
                command.Parameters.AddWithValue("@AddressLine1", customer.AddressLine1);
                command.Parameters.AddWithValue("@AddressLine2", customer.AddressLine2);
                command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                command.Parameters.AddWithValue("@Phone", customer.Phone);
                command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                command.Parameters.AddWithValue("@Active", customer.Active); // Ensure this is set correctly
                command.Parameters.AddWithValue("@CreateDate", currentDate);
                command.Parameters.AddWithValue("@CreatedBy", "SystemUser");
                command.Parameters.AddWithValue("@LastUpdate", currentDate);
                command.Parameters.AddWithValue("@LastUpdateBy", "SystemUser");

                command.ExecuteNonQuery();
            }
        }


        // Update an existing customer and associated country, city, and address
        public void UpdateCustomer(Customer customer)
        {
            // First, update the Address
            string addressQuery = @"UPDATE address 
                            SET Address = @AddressLine1, Address2 = @AddressLine2, PostalCode = @PostalCode, Phone = @Phone 
                            WHERE AddressId = @AddressId";

            using (var addressCommand = new MySqlCommand(addressQuery, _context.Connection))
            {
                addressCommand.Parameters.AddWithValue("@AddressLine1", customer.AddressLine1);
                addressCommand.Parameters.AddWithValue("@AddressLine2", customer.AddressLine2);
                addressCommand.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
                addressCommand.Parameters.AddWithValue("@Phone", customer.Phone);
                addressCommand.Parameters.AddWithValue("@AddressId", customer.AddressId);

                addressCommand.ExecuteNonQuery();
            }

            // Now, update the Customer
            string customerQuery = @"UPDATE customer 
                             SET CustomerName = @CustomerName, AddressId = @AddressId, Active = @Active 
                             WHERE CustomerId = @CustomerId";

            using (var customerCommand = new MySqlCommand(customerQuery, _context.Connection))
            {
                customerCommand.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                customerCommand.Parameters.AddWithValue("@AddressId", customer.AddressId);
                customerCommand.Parameters.AddWithValue("@Active", customer.Active);
                customerCommand.Parameters.AddWithValue("@CustomerId", customer.CustomerId);

                customerCommand.ExecuteNonQuery();
            }
        }

        // Delete a customer and associated address, city, and country
        public void DeleteCustomer(int customerId)
        {
            string query = @"DELETE FROM customer WHERE CustomerId = @CustomerId;

                             DELETE FROM address WHERE AddressId = (SELECT AddressId FROM customer WHERE CustomerId = @CustomerId);

                             DELETE FROM city WHERE CityId = (SELECT CityId FROM address WHERE AddressId = (SELECT AddressId FROM customer WHERE CustomerId = @CustomerId));

                             DELETE FROM country WHERE CountryId = (SELECT CountryId FROM city WHERE CityId = (SELECT CityId FROM address WHERE AddressId = (SELECT AddressId FROM customer WHERE CustomerId = @CustomerId)));";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.ExecuteNonQuery();
            }
        }
    }
}
