
namespace ConcertAPI.Models;

public class PurchasedTicket
{
    public int TicketConcertId { get; set; }
    public TicketConcert TicketConcert { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateTime PurchaseDate { get; set; }
}