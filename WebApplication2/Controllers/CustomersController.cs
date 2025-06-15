using ConcertAPI.Dtos;
using ConcertAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConcertAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IConcertService _service;
    public CustomersController(IConcertService service) => _service = service;

    [HttpGet("{customerId}/purchases")]
    public async Task<IActionResult> GetPurchases(int customerId)
    {
        var result = await _service.GetCustomerPurchasesAsync(customerId);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomerWithTickets([FromBody] CustomerRequestDto request)
    {
        var (success, error) = await _service.AddCustomerWithTicketsAsync(request);
        if (!success)
        {
            if (error.Contains("already exists"))
                return Conflict(error);
            if (error.Contains("does not exist"))
                return NotFound(error);
            return BadRequest(error);
        }
        return Created("", null);
    }
}

