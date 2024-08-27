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
            var query = @"SELECT a.AppointmentId, a.CustomerId, a.UserId, 
                         a.Title, a.Description, a.Location, a.Contact, 
                         a.Type, a.Start, a.End, 
                         a.CreateDate, a.CreatedBy, a.LastUpdate, a.LastUpdateBy,
                         c.CustomerName, u.UserName 
                  FROM appointment a
                  INNER JOIN customer c ON a.CustomerId = c.CustomerId
                  INNER JOIN user u ON a.UserId = u.UserId";

            using (var dataAdapter = new MySqlDataAdapter(query, _context.Connection))
            {
                dataAdapter.Fill(dataTable);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                row["Start"] = Convert.ToDateTime(row["Start"]).ToLocalTime();
                row["End"] = Convert.ToDateTime(row["End"]).ToLocalTime();
            }
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["AppointmentId"] };
            return dataTable;
        }


        public Appointment GetAppointmentById(int appointmentId)
        {
            try
            {
                var allAppointments = GetAllAppointments();
                var appointmentRow = allAppointments.Rows.Find(appointmentId);

                if (appointmentRow == null)
                {
                    throw new Exception($"Appointment with ID {appointmentId} not found.");
                }

                var appointment = new Appointment
                {
                    AppointmentId = (int)appointmentRow["AppointmentId"],
                    CustomerId = (int)appointmentRow["CustomerId"],
                    UserId = (int)appointmentRow["UserId"],
                    Title = appointmentRow["Title"].ToString(),
                    Description = appointmentRow["Description"].ToString(),
                    Location = appointmentRow["Location"].ToString(),
                    Contact = appointmentRow["Contact"].ToString(),
                    Type = appointmentRow["Type"].ToString(),
                    Start = (DateTime)appointmentRow["Start"],
                    End = (DateTime)appointmentRow["End"],
                    CreateDate = (DateTime)appointmentRow["CreateDate"],
                    CreatedBy = appointmentRow["CreatedBy"].ToString(),
                    LastUpdate = (DateTime)appointmentRow["LastUpdate"],
                    LastUpdateBy = appointmentRow["LastUpdateBy"].ToString(),
                };

                return appointment;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                MessageBox.Show($"Error fetching appointment: {ex.ToString()}");
                return null;
            }
        }
        public void AddAppointment(Appointment appointment)
        {
            try
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
                    command.Parameters.AddWithValue("@Url", appointment.Url ?? "default-url.com");
                    command.Parameters.AddWithValue("@Start", appointment.Start);
                    command.Parameters.AddWithValue("@End", appointment.End);
                    command.Parameters.AddWithValue("@CreateDate", appointment.CreateDate);
                    command.Parameters.AddWithValue("@CreatedBy", appointment.CreatedBy ?? "default name");
                    command.Parameters.AddWithValue("@LastUpdate", appointment.LastUpdate);
                    command.Parameters.AddWithValue("@LastUpdateBy", appointment.LastUpdateBy ?? "default name");

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("There was an issue saving that appointment. " + ex.ToString());
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
            try
            {
                using (var command = new MySqlCommand(query, _context.Connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", appointment.CustomerId);
                    command.Parameters.AddWithValue("@UserId", appointment.UserId);
                    command.Parameters.AddWithValue("@Title", appointment.Title);
                    command.Parameters.AddWithValue("@Description", appointment.Description);
                    command.Parameters.AddWithValue("@Location", appointment.Location);
                    command.Parameters.AddWithValue("@Contact", appointment.Contact);
                    command.Parameters.AddWithValue("@Type", appointment.Type);
                    command.Parameters.AddWithValue("@Url", appointment.Url ?? "default-url.com");
                    command.Parameters.AddWithValue("@Start", appointment.Start);
                    command.Parameters.AddWithValue("@End", appointment.End);
                    command.Parameters.AddWithValue("@LastUpdate", appointment.LastUpdate);
                    command.Parameters.AddWithValue("@LastUpdateBy", appointment.LastUpdateBy ?? "default name");
                    command.Parameters.AddWithValue("@AppointmentId", appointment.AppointmentId);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an issue saving that change. {ex.ToString()}");
            }
        }

        public void DeleteAppointment(int appointmentId)
        {
            var query = @"DELETE FROM appointment WHERE AppointmentId = @AppointmentId";

            try
            {
                using (var command = new MySqlCommand(query, _context.Connection))
                {
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an issue deleting that appointment. {ex.ToString()}");
            }
        }

    }
}
