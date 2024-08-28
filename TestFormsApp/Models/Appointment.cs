using System;

namespace C969App.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        // Placeholder values for the rest of the fields
        public string Title { get; set; } = "Not Applicable";
        public string Description { get; set; } = "Not Applicable";
        public string Location { get; set; } = "Not Applicable";
        public string Contact { get; set; } = "Not Applicable";
        public string Url { get; set; } = "http://placeholder.com";

        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
    }
    //Decided to use a DTO here because I'm getting mad at finding workarounds for everything.
    public class AppointmentDTO
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
