﻿namespace ConcertAPI.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public List<PurchasedTicket> PurchasedTickets { get; set; }
}
