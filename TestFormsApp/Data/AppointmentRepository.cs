using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using C969App.Models;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace C969App.Data
{
    public class AppointmentRepository
    {
        private readonly DbContext _context;

        public AppointmentRepository(DbContext context)
        {
            _context = context;
        }

        public DataTable GetAllAppointments()
        {
            var dataTable = new DataTable();
            var query = @"SELECT a.AppointmentId, c.CustomerName, u.UserName, 
                         a.Title, a.Location, a.Contact, a.Type, 
                         a.Start, a.End
                  FROM appointment a
                  INNER JOIN customer c ON a.CustomerId = c.CustomerId
                  INNER JOIN user u ON a.UserId = u.UserId";

            using (var dataAdapter = new MySqlDataAdapter(query, _context.Connection))
            {
                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }


        public Appointment GetAppointmentById(int appointmentId)
        {
            try
            {
                var allAppointments = GetAllAppointments();
                var appointmentRow = allAppointments.Rows.Find(appointmentId);

                var appointment = new Appointment()
                {
                    AppointmentId = (int)appointmentRow["AppointmentId"]
                };

                if (appointment == null)
                {
                    throw new Exception($"Appointment with ID {appointmentId} not found.");
                }

                return appointment;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                MessageBox.Show($"Error fetching appointment: {ex.Message}");
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
