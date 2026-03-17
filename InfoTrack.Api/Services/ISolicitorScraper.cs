using InfoTrack.Shared.Models;

namespace InfoTrack.Api.Services;

public interface ISolicitorScraper
{
    Task<List<Solicitor>> GetSolicitorsAsync(List<string> locations);
}