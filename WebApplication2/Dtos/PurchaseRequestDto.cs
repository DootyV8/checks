namespace ConcertAPI.Dtos;


public class PurchaseRequestDto
{
    public int SeatNumber { get; set; }
    public string ConcertName { get; set; }
    public decimal Price { get; set; }
}