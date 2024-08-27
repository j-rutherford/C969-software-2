using System;

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int AddressId { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; }
    public int CountryId { get; set; }
    public string CountryName { get; set; }
    public bool Active { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastUpdate { get; set; }
    public string LastUpdateBy { get; set; }
}
