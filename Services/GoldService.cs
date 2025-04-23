using System.Net.Http.Json;
using front__wasm.Models;

namespace front__wasm.Services
{
  public class GoldService
  {
    private readonly HttpClient _httpClient;
    private readonly ILogger<GoldService>? _logger;

    public GoldService(HttpClient httpClient, ILogger<GoldService>? logger = null)
    {
      _httpClient = httpClient;
      _logger = logger;
    }

    public async Task<GoldServiceDetails?> GetGoldDetailsAsync()
    {
      try
      {
        var details = await _httpClient.GetFromJsonAsync<GoldServiceDetails>("data/gold.json");
        _logger?.LogInformation("Loaded gold service details successfully.");
        return details;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, "Error loading gold service details: {Message}", ex.Message);
        Console.WriteLine($"Error loading gold service details: {ex.Message}");
        return null;
      }
    }
  }
}
