using System.Net.Http.Json;
using front__wasm.Models;

namespace front__wasm.Services
{
  public class RaidService
  {
    private readonly HttpClient _httpClient;
    private readonly ILogger<RaidService>? _logger;

    public RaidService(HttpClient httpClient, ILogger<RaidService>? logger = null)
    {
      _httpClient = httpClient;
      _logger = logger;
    }

    public async Task<RaidServiceDetails?> GetRaidDetailsAsync()
    {
      try
      {
        var details = await _httpClient.GetFromJsonAsync<RaidServiceDetails>("data/raids.json");
        _logger?.LogInformation("Loaded Raid service details successfully.");
        return details;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, "Error loading Raid service details: {Message}", ex.Message);
        Console.WriteLine($"Error loading Raid service details: {ex.Message}");
        return null;
      }
    }
  }
}
