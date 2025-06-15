using ConcertAPI.Data;
using ConcertAPI.Dtos;
using ConcertAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertAPI.Services;


public class ConcertService : IConcertService
{
    private readonly AppDbContext _context;
    public ConcertService(AppDbContext context) => _context = context; //dependency injection

    public async Task<PurchaseResponseDto> GetCustomerPurchasesAsync(int customerId)
    {
        return await _context.Customers
            .Where(c => c.CustomerId == customerId)
            .Select(c => new PurchaseResponseDto
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                Purchases = c.PurchasedTickets.Select(pt => new PurchaseDto
                {
                    Date = pt.PurchaseDate,
                    Price = pt.TicketConcert.Price,
                    Ticket = new TicketDto
                    {
                        Serial = pt.TicketConcert.Ticket.SerialNumber,
                        SeatNumber = pt.TicketConcert.Ticket.SeatNumber
                    },
                    Concert = new ConcertDto
                    {
                        Name = pt.TicketConcert.Concert.Name,
                        Date = pt.TicketConcert.Concert.Date
                    }
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<(bool Success, string Error)> AddCustomerWithTicketsAsync(CustomerRequestDto request)
    {
        if (await _context.Customers.AnyAsync(c => c.CustomerId == request.Customer.Id))
            return (false, "Customer already exists.");

        var newCustomer = new Customer
        {
            CustomerId = request.Customer.Id,
            FirstName = request.Customer.FirstName,
            LastName = request.Customer.LastName,
            PhoneNumber = request.Customer.PhoneNumber
        };

        _context.Customers.Add(newCustomer);
        await _context.SaveChangesAsync();

        foreach (var purchase in request.Purchases)
        {
            var concert = await _context.Concerts.FirstOrDefaultAsync(x => x.Name == purchase.ConcertName);
            if (concert == null)
                return (false, $"Concert '{purchase.ConcertName}' does not exist.");

            var ticketsForConcert = await _context.PurchasedTickets
                .Include(pt => pt.TicketConcert)
                .CountAsync(pt => pt.CustomerId == newCustomer.CustomerId && pt.TicketConcert.ConcertId == concert.ConcertId);
            if (ticketsForConcert >= 5)
                return (false, "Cannot buy more than 5 tickets for a single concert.");

            var serial = Guid.NewGuid().ToString();
            var newTicket = new Ticket
            {
                SerialNumber = serial,
                SeatNumber = purchase.SeatNumber
            };
            _context.Tickets.Add(newTicket);
            await _context.SaveChangesAsync();

            var newTicketConcert = new TicketConcert
            {
                TicketId = newTicket.TicketId,
                ConcertId = concert.ConcertId,
                Price = purchase.Price
            };
            _context.TicketConcerts.Add(newTicketConcert);
            await _context.SaveChangesAsync();

            var purchasedTicket = new PurchasedTicket
            {
                TicketConcertId = newTicketConcert.TicketConcertId,
                CustomerId = newCustomer.CustomerId,
                PurchaseDate = DateTime.UtcNow
            };
            _context.PurchasedTickets.Add(purchasedTicket);
            await _context.SaveChangesAsync();
        }

        return (true, null);
    }
}
