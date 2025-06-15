using ConcertAPI.Dtos;

namespace ConcertAPI.Services;

public interface IConcertService
{
    Task<PurchaseResponseDto> GetCustomerPurchasesAsync(int customerId);
    Task<(bool Success, string Error)> AddCustomerWithTicketsAsync(CustomerRequestDto request);
}
