using System.Net.Http.Json;
using front__wasm.Models;

namespace front__wasm.Services
{
  public class PvpService
  {
    private readonly HttpClient _httpClient;
    private readonly ILogger<PvpService>? _logger;

    public PvpService(HttpClient httpClient, ILogger<PvpService>? logger = null)
    {
      _httpClient = httpClient;
      _logger = logger;
    }

    public async Task<PvpServiceDetails?> GetPvpDetailsAsync()
    {
      try
      {
        var details = await _httpClient.GetFromJsonAsync<PvpServiceDetails>("data/pvp.json");
        _logger?.LogInformation("Loaded PvP service details successfully.");
        return details;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, "Error loading PvP service details: {Message}", ex.Message);
        Console.WriteLine($"Error loading PvP service details: {ex.Message}");
        return null;
      }
    }
  }
}
