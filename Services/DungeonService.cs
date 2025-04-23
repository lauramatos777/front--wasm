using System.Net.Http.Json;
using front__wasm.Models;

namespace front__wasm.Services
{
  public class DungeonService
  {
    private readonly HttpClient _httpClient;
    private readonly ILogger<DungeonService>? _logger;

    public DungeonService(HttpClient httpClient, ILogger<DungeonService>? logger = null)
    {
      _httpClient = httpClient;
      _logger = logger;
    }

    public async Task<DungeonServiceDetails?> GetDungeonDetailsAsync()
    {
      try
      {
        var details = await _httpClient.GetFromJsonAsync<DungeonServiceDetails>("data/dungeons.json");
        _logger?.LogInformation("Loaded dungeon service details successfully.");
        return details;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, "Error loading dungeon service details: {Message}", ex.Message);
        Console.WriteLine($"Error loading dungeon service details: {ex.Message}");
        return null;
      }
    }
  }
}
