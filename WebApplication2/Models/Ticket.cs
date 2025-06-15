namespace ConcertAPI.Models;

public class Ticket
{
    public int TicketId { get; set; }
    public string SerialNumber { get; set; }
    public int SeatNumber { get; set; }
    public List<TicketConcert> TicketConcerts { get; set; }
}
