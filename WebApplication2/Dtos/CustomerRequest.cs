namespace ConcertAPI.Dtos;

public class CustomerRequestDto
{
    public CustomerDto Customer { get; set; }
    public List<PurchaseRequestDto> Purchases { get; set; }
}