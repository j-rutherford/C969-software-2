using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using C969App.Models;
using System.Linq;

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
            var query = @"SELECT * FROM appointment";

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
                        LastUpdateBy = reader.GetString("LastUpdateBy"),
                        // Populate navigation properties if needed
                    };
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }



        public Appointment GetAppointmentById(int appointmentId)
        {
            try
            {
                var allAppointments = GetAllAppointments();
                var appointment = allAppointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

                if (appointment == null)
                {
                    throw new Exception($"Appointment with ID {appointmentId} not found.");
                }

                return appointment;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error fetching appointment: {ex.Message}");
                return null;
            }
        }
        public void AddAppointment(Appointment appointment)
        {

                var query = @"INSERT INTO appointment (CustomerId, UserId, Title, Description, Location, Contact, Type, Url, Start, End, CreateDate, CreatedBy, LastUpdate, LastUpdateBy) 
                              VALUES (@CustomerId, @UserId, @Title, @Description, @Location, @Contact, @Type, @Url, @Start, @End, @CreateDate, @CreatedBy, @LastUpdate, @LastUpdateBy)";
                using (var command = new MySqlCommand(query, _context.Connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", appointment.CustomerId);
                    command.Parameters.AddWithValue("@UserId", appointment.UserId);
                    command.Parameters.AddWithValue("@Title", appointment.Title);
                    command.Parameters.AddWithValue("@Description", appointment.Description);
                    command.Parameters.AddWithValue("@Location", appointment.Location);
                    command.Parameters.AddWithValue("@Contact", appointment.Contact);
                    command.Parameters.AddWithValue("@Type", appointment.Type);
                    command.Parameters.AddWithValue("@Url", appointment.Url);
                    command.Parameters.AddWithValue("@Start", appointment.Start);
                    command.Parameters.AddWithValue("@End", appointment.End);
                    command.Parameters.AddWithValue("@CreateDate", appointment.CreateDate);
                    command.Parameters.AddWithValue("@CreatedBy", appointment.CreatedBy);
                    command.Parameters.AddWithValue("@LastUpdate", appointment.LastUpdate);
                    command.Parameters.AddWithValue("@LastUpdateBy", appointment.LastUpdateBy);

                    command.ExecuteNonQuery();
                }
            }
        

        public void UpdateAppointment(Appointment appointment)
        {

                var query = @"UPDATE appointment 
                      SET CustomerId = @CustomerId, 
                          UserId = @UserId, 
                          Title = @Title, 
                          Description = @Description, 
                          Location = @Location, 
                          Contact = @Contact, 
                          Type = @Type, 
                          Url = @Url, 
                          Start = @Start, 
                          End = @End, 
                          LastUpdate = @LastUpdate, 
                          LastUpdateBy = @LastUpdateBy 
                      WHERE AppointmentId = @AppointmentId";
                using (var command = new MySqlCommand(query, _context.Connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", appointment.CustomerId);
                    command.Parameters.AddWithValue("@UserId", appointment.UserId);
                    command.Parameters.AddWithValue("@Title", appointment.Title);
                    command.Parameters.AddWithValue("@Description", appointment.Description);
                    command.Parameters.AddWithValue("@Location", appointment.Location);
                    command.Parameters.AddWithValue("@Contact", appointment.Contact);
                    command.Parameters.AddWithValue("@Type", appointment.Type);
                    command.Parameters.AddWithValue("@Url", appointment.Url);
                    command.Parameters.AddWithValue("@Start", appointment.Start);
                    command.Parameters.AddWithValue("@End", appointment.End);
                    command.Parameters.AddWithValue("@LastUpdate", appointment.LastUpdate);
                    command.Parameters.AddWithValue("@LastUpdateBy", appointment.LastUpdateBy);
                    command.Parameters.AddWithValue("@AppointmentId", appointment.AppointmentId);

                    command.ExecuteNonQuery();
                
            }
        }

        public void DeleteAppointment(int appointmentId)
        {
            var query = @"DELETE FROM appointment WHERE AppointmentId = @AppointmentId";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                command.ExecuteNonQuery();
            }

        }

    }
}
