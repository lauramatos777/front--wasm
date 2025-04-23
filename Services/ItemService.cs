using System.Net.Http.Json;
using front__wasm.Models;

namespace front__wasm.Services
{
  public class ItemService
  {
    private readonly HttpClient _httpClient;
    private readonly ILogger<ItemService>? _logger;

    public ItemService(HttpClient httpClient, ILogger<ItemService>? logger = null)
    {
      _httpClient = httpClient;
      _logger = logger;
    }

    public async Task<ItemServiceDetails?> GetItemDetailsAsync()
    {
      try
      {
        var details = await _httpClient.GetFromJsonAsync<ItemServiceDetails>("data/itens.json");
        _logger?.LogInformation("Loaded item service details successfully.");
        return details;
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, "Error loading item service details: {Message}", ex.Message);
        Console.WriteLine($"Error loading item service details: {ex.Message}");
        return null;
      }
    }
  }
}
