using InfoTrack.Api.Services;
using Microsoft.AspNetCore.Mvc;
using InfoTrack.Api.Data;

namespace InfoTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitorsController : ControllerBase
{
    private readonly ISolicitorScraper _scraper;

    public SolicitorsController(ISolicitorScraper scraper)
    {
        _scraper = scraper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string locations)
    {
        var locationList = locations?
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(l => l.Trim())
            .ToList() ?? new List<string>();

        var result = await _scraper.GetSolicitorsAsync(locationList);

        return Ok(result);
    }

    [HttpGet("history")]
    public IActionResult GetHistory([FromServices] AppDbContext db)
    {
        var data = db.Solicitors.ToList();
        return Ok(data);
    }

    [HttpGet("report")]
    public IActionResult GetReport([FromServices] AppDbContext db)
    {
        var data = db.Solicitors.ToList();

        var report = new
        {
            TotalSolicitors = data.Count,

            ByLocation = data
                .GroupBy(x => x.Location)
                .Select(g => new
                {
                    Location = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count),

            MissingPhones = data.Count(x => x.Phone == "N/A")
        };

        return Ok(report);
    }
}