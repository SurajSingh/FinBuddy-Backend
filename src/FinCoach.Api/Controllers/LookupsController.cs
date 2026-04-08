using FinCoach.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinCoach.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LookupsController : ControllerBase
{
    private readonly AppDbContext _context;

    public LookupsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        var countries = await _context.Countries
            .Where(c => c.IsEnabled)
            .OrderBy(c => c.Name)
            .ToListAsync();
        return Ok(countries);
    }

    [HttpGet("currencies")]
    public async Task<IActionResult> GetCurrencies()
    {
        var currencies = await _context.Currencies
            .Where(c => c.IsEnabled)
            .OrderBy(c => c.Code)
            .ToListAsync();
        return Ok(currencies);
    }
}
