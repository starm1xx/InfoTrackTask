using System.Text.RegularExpressions;
using InfoTrack.Shared.Models;
using InfoTrack.Api.Data;

namespace InfoTrack.Api.Services;

public class SolicitorScraper : ISolicitorScraper
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _db;

    public SolicitorScraper(HttpClient httpClient, AppDbContext db)
    {
        _httpClient = httpClient;
        _db = db;

        _httpClient.DefaultRequestHeaders.Add("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 " +
            "(KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");

        _httpClient.DefaultRequestHeaders.Add("Accept",
            "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
    }

    public async Task<List<Solicitor>> GetSolicitorsAsync(List<string> locations)
    {
        var results = new List<Solicitor>();

        foreach (var location in locations)
        {
            var formattedLocation = location.ToLower().Replace(" ", "-");
            var url = $"https://www.solicitors.com/{formattedLocation}-solicitors.html";

            Console.WriteLine($"🔍 Requesting: {url}");

            var html = await _httpClient.GetStringAsync(url);

            Console.WriteLine("===== HTML START =====");
            var previewLength = Math.Min(html.Length, 1000);
            Console.WriteLine(html.Substring(0, previewLength));
            Console.WriteLine("===== HTML END =====");

            if (!html.Contains("Solicitors"))
            {
                Console.WriteLine("⚠️ No expected content found!");
            }

            var parsed = ParseHtml(html, location);

            Console.WriteLine($"✅ Parsed {parsed.Count} results for {location}");

            results.AddRange(parsed);
        }

        foreach (var solicitor in results)
        {
            if (!_db.Solicitors.Any(x =>
                x.Name == solicitor.Name &&
                x.Location == solicitor.Location))
            {
                _db.Solicitors.Add(solicitor);
            }
        }

        await _db.SaveChangesAsync();

        return results;
    }

    private List<Solicitor> ParseHtml(string html, string location)
    {


        var list = new List<Solicitor>();

        var blocks = html.Split("result-item");

        foreach (var block in blocks)
        {
         
            var nameMatch = Regex.Match(block,
                @"<span class=""h2"">(.*?)</span>",
                RegexOptions.IgnoreCase);

            if (!nameMatch.Success)
                continue;

            var name = Clean(nameMatch.Groups[1].Value);

            var phoneMatch = Regex.Match(block,
                @"tel:(.*?)""",
                RegexOptions.IgnoreCase);

            var phone = phoneMatch.Success
                ? FormatPhone(phoneMatch.Groups[1].Value)
                : "N/A";

            var urlMatch = Regex.Match(block,
                @"<a href=""(.*?)"" class=""link-map""",
                RegexOptions.IgnoreCase);

            var profileUrl = urlMatch.Success
                ? $"https://www.solicitors.com{urlMatch.Groups[1].Value}"
                : "";

            var addressMatch = Regex.Match(block,
                @"<address>(.*?)</address>",
                RegexOptions.IgnoreCase);

            var address = addressMatch.Success
                ? Clean(addressMatch.Groups[1].Value)
                : "";

            var websiteMatch = Regex.Match(block,
                @"<a[^>]*target=""_blank""[^>]*href=""(http.*?)""",
                RegexOptions.IgnoreCase);

            var website = websiteMatch.Success
                ? websiteMatch.Groups[1].Value
                : "";

            var emailMatch = Regex.Match(block,
                @"href=""(/enquiry-form.*?)""",
                RegexOptions.IgnoreCase);

            var email = emailMatch.Success
                ? $"https://www.solicitors.com{emailMatch.Groups[1].Value}"
                : "";
            list.Add(new Solicitor
            {
                Name = name,
                Location = location,
                Phone = phone,
                ProfileUrl = profileUrl,
                Address = address,
                Website = website,
                Email = email,
            });
        }

        return list
            .Where(x => !string.IsNullOrWhiteSpace(x.Name))
            .GroupBy(x => x.Name)
            .Select(g => g.First())
            .ToList();
    }

    private string Clean(string input)
    {
        var cleaned = System.Net.WebUtility.HtmlDecode(
            Regex.Replace(input, "<.*?>", "").Trim()
        );

        cleaned = Regex.Replace(cleaned, @"\(\d+\)", "").Trim();

        return cleaned;
    }

    private string FormatPhone(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return "N/A";

        raw = raw.Replace("tel:", "");

        raw = raw.Replace(" ", "")
                 .Replace("(0)", "")
                 .Trim();

        if (!raw.StartsWith("0"))
            raw = "0" + raw;

        return raw;
    }
}