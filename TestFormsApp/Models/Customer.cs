using C969App.Models;
using System;
using System.Collections.Generic;

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int AddressId { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public int CountryId { get; set; }
    public string CountryName { get; set; }

    public virtual List<Appointment> Appointments { get; set; }
}
