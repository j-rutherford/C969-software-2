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

        // Retrieve all appointments with associated customer and user information
        public DataTable GetAllAppointments()
        {
            var dataTable = new DataTable();
            var query = @"SELECT a.AppointmentId, 
                         c.CustomerId, c.CustomerName, 
                         u.UserId, u.UserName, 
                         a.Type, DATE(a.Start) AS AppointmentDate, 
                         TIME(a.Start) AS StartTime, TIME(a.End) AS EndTime
                  FROM appointment a
                  INNER JOIN customer c ON a.CustomerId = c.CustomerId
                  INNER JOIN user u ON a.UserId = u.UserId";

            using (var dataAdapter = new MySqlDataAdapter(query, _context.Connection))
            {
                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        // Retrieve a specific appointment by its ID
        public AppointmentDTO GetAppointmentById(int appointmentId)
        {
            try
            {
                // Get all appointments as a DataTable
                var allAppointments = GetAllAppointments();

                // Use LINQ with a lambda expression to find the specific appointment by ID
                var appointmentRow = allAppointments.AsEnumerable()
                    .FirstOrDefault(row => row.Field<int>("AppointmentId") == appointmentId);

                if (appointmentRow == null)
                {
                    throw new Exception($"Appointment with ID {appointmentId} not found.");
                }

                // Convert and combine the date and time for Start and End
                DateTime appointmentDate = appointmentRow.Field<DateTime>("AppointmentDate");
                TimeSpan startTime = (TimeSpan)appointmentRow["StartTime"];
                TimeSpan endTime = (TimeSpan)appointmentRow["EndTime"];

                // Combine date with time, and convert to local time
                DateTime startDateTime = appointmentDate.Add(startTime).ToLocalTime();
                DateTime endDateTime = appointmentDate.Add(endTime).ToLocalTime();

                // Create the AppointmentDTO from the DataRow
                var appointment = new AppointmentDTO
                {
                    AppointmentId = appointmentRow.Field<int>("AppointmentId"),
                    CustomerId = appointmentRow.Field<int>("CustomerId"),
                    UserId = appointmentRow.Field<int>("UserId"),
                    CustomerName = appointmentRow.Field<string>("CustomerName"),
                    UserName = appointmentRow.Field<string>("UserName"),
                    Type = appointmentRow.Field<string>("Type"),
                    Start = startDateTime,
                    End = endDateTime
                };

                return appointment;
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show($"Invalid data type conversion: {ex.Message}", "Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching appointment: {ex.Message}");
                return null;
            }
        }

        // Add a new appointment
        public void AddAppointment(AppointmentDTO appointment)
        {
            string query = @"INSERT INTO appointment (CustomerId, UserId, Type,Title, Start, End, CreateDate, CreatedBy, LastUpdate, LastUpdateBy, Description, Location, Contact, Url) 
                             VALUES (@CustomerId, @UserId, @Type,'[N/A]', @Start, @End, NOW(), 'ADMIN', NOW(), 'ADMIN','[N/A]','[N/A]','[N/A]','[N/A]')";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@CustomerId", appointment.CustomerId);
                command.Parameters.AddWithValue("@UserId", appointment.UserId);
                command.Parameters.AddWithValue("@Type", appointment.Type);
                command.Parameters.AddWithValue("@Start", appointment.Start);
                command.Parameters.AddWithValue("@End", appointment.End);

                command.ExecuteNonQuery();
            }
        }

        // Update an existing appointment
        public void UpdateAppointment(AppointmentDTO appointment)
        {
            string query = @"UPDATE appointment 
                             SET CustomerId = @CustomerId, 
                                 UserId = @UserId, 
                                 Type = @Type, 
                                 Start = @Start, 
                                 End = @End, 
                                 LastUpdate = NOW(), 
                                 LastUpdateBy = 'ADMIN' 
                             WHERE AppointmentId = @AppointmentId";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@CustomerId", appointment.CustomerId);
                command.Parameters.AddWithValue("@UserId", appointment.UserId);
                command.Parameters.AddWithValue("@Type", appointment.Type);
                command.Parameters.AddWithValue("@Start", appointment.Start);
                command.Parameters.AddWithValue("@End", appointment.End);
                command.Parameters.AddWithValue("@AppointmentId", appointment.AppointmentId);

                command.ExecuteNonQuery();
            }
        }

        // Delete an appointment
        public void DeleteAppointment(int appointmentId)
        {
            string query = @"DELETE FROM appointment WHERE AppointmentId = @AppointmentId";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                command.ExecuteNonQuery();
            }
        }

        public List<AppointmentDTO> GetAppointmentsByUser(int userId)
        {
            var appointments = new List<AppointmentDTO>();
            var query = @"SELECT a.AppointmentId, c.CustomerName, u.UserName, 
                         a.Type, a.Start, a.End
                  FROM appointment a
                  INNER JOIN customer c ON a.CustomerId = c.CustomerId
                  INNER JOIN user u ON a.UserId = u.UserId
                  WHERE a.UserId = @UserId";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var appointment = new AppointmentDTO
                        {
                            AppointmentId = reader.GetInt32("AppointmentId"),
                            CustomerName = reader.GetString("CustomerName"),
                            UserName = reader.GetString("UserName"),
                            Type = reader.GetString("Type"),
                            Start = reader.GetDateTime("Start"),
                            End = reader.GetDateTime("End")
                        };
                        appointments.Add(appointment);
                    }
                }
            }
            return appointments;
        }
        public DataTable GetMonthlyAppointmentReport()
        {
            var dataTable = new DataTable();
            var query = @"
                SELECT 
                    DATE_FORMAT(Start, '%Y-%m') AS Month, 
                    Type, 
                    COUNT(*) AS Count
                FROM appointment
                GROUP BY Month, Type
                ORDER BY Month, Type";

            using (var dataAdapter = new MySqlDataAdapter(query, _context.Connection))
            {
                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }


    }
}
