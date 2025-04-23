using System.Net.Http.Json;
using front__wasm.Models;


namespace front__wasm.Services
{
  public class CatalogService
  {
    private readonly HttpClient _httpClient;
    private readonly ILogger<CatalogService>? _logger;

    public CatalogService(HttpClient httpClient, ILogger<CatalogService>? logger = null)
    {
      _httpClient = httpClient;
      _logger = logger;
    }

    public async Task<IEnumerable<CatalogServiceDetails>> GetServicesAsync()
    {
      try
      {
        var services = await _httpClient.GetFromJsonAsync<IEnumerable<CatalogServiceDetails>>("data/services.json");

        _logger?.LogInformation($"Loaded {services?.Count() ?? 0} services");

        return services ?? new List<CatalogServiceDetails>();
      }
      catch (Exception ex)
      {
        _logger?.LogError(ex, "Erro ao carregar serviços: {Message}", ex.Message);
        Console.WriteLine($"Error loading services: {ex.Message}");
        return new List<CatalogServiceDetails>();
      }
    }
  }
}
