using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using C969App.Models;

namespace C969App.Data
{
    public class AppointmentRepository
    {
        private readonly DbContext _context;

        public AppointmentRepository(DbContext context)
        {
            _context = context;
        }

        public List<Appointment> GetAllAppointments()
        {
            var appointments = new List<Appointment>();
            string query = "SELECT * FROM appointment";
            using (var command = new MySqlCommand(query, _context.Connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var appointment = new Appointment
                    {
                        AppointmentId = reader.GetInt32("AppointmentId"),
                        CustomerId = reader.GetInt32("CustomerId"),
                        UserId = reader.GetInt32("UserId"),
                        Title = reader.GetString("Title"),
                        Description = reader.GetString("Description"),
                        Location = reader.GetString("Location"),
                        Contact = reader.GetString("Contact"),
                        Type = reader.GetString("Type"),
                        Url = reader.GetString("Url"),
                        Start = reader.GetDateTime("Start"),
                        End = reader.GetDateTime("End"),
                        CreateDate = reader.GetDateTime("CreateDate"),
                        CreatedBy = reader.GetString("CreatedBy"),
                        LastUpdate = reader.GetDateTime("LastUpdate"),
                        LastUpdateBy = reader.GetString("LastUpdateBy")
                    };
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }

        // Additional CRUD methods for Appointment
    }
}
