namespace ConcertAPI.Models;

public class TicketConcert
{
    public int TicketConcertId { get; set; }
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
    public int ConcertId { get; set; }
    public Concert Concert { get; set; }
    public decimal Price { get; set; }
    public List<PurchasedTicket> PurchasedTickets { get; set; }
}
