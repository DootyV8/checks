namespace ConcertAPI.Models;

public class Concert
{
    public int ConcertId { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int AvailableTickets { get; set; }
    public List<TicketConcert> TicketConcerts { get; set; }
}
