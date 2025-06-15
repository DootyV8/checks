using ConcertAPI.Models;
using Microsoft.EntityFrameworkCore;
using ConcertAPI.Models;

namespace ConcertAPI.Data;



public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Concert> Concerts { get; set; }
    public DbSet<TicketConcert> TicketConcerts { get; set; }
    public DbSet<PurchasedTicket> PurchasedTickets { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PurchasedTicket>()
            .HasKey(pt => new { pt.TicketConcertId, pt.CustomerId });
        
        modelBuilder.Entity<TicketConcert>()
            .Property(tc => tc.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", PhoneNumber = null }
        );
        modelBuilder.Entity<Concert>().HasData(
            new Concert { ConcertId = 1, Name = "Concert 1", Date = DateTime.Parse("2025-06-09T09:00:00"), AvailableTickets = 300 },
            new Concert { ConcertId = 2, Name = "Concert 14", Date = DateTime.Parse("2025-06-10T09:00:00"), AvailableTickets = 500 }
        );
        modelBuilder.Entity<Ticket>().HasData(
            new Ticket { TicketId = 1, SerialNumber = "TK2034/S4531/12", SeatNumber = 124 },
            new Ticket { TicketId = 2, SerialNumber = "TK2027/S4831/133", SeatNumber = 330 }
        );
        modelBuilder.Entity<TicketConcert>().HasData(
            new TicketConcert { TicketConcertId = 1, TicketId = 1, ConcertId = 1, Price = 33.4M },
            new TicketConcert { TicketConcertId = 2, TicketId = 2, ConcertId = 2, Price = 48.4M }
        );
        modelBuilder.Entity<PurchasedTicket>().HasData(
            new PurchasedTicket { TicketConcertId = 1, CustomerId = 1, PurchaseDate = DateTime.Parse("2025-06-09T09:00:00") },
            new PurchasedTicket { TicketConcertId = 2, CustomerId = 1, PurchaseDate = DateTime.Parse("2025-06-09T09:00:00") }
        );
    }
}
