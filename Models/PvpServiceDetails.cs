namespace front__wasm.Models
{
  public class PvpServiceDetails
  {
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string FeaturesTitle { get; set; } = "";
    public List<string> Features { get; set; } = new List<string>();
    public string DetailsTitle { get; set; } = "";
    public List<string> Details { get; set; } = new List<string>();
    public string PackagesTitle { get; set; } = "";
    public List<Package> Packages { get; set; } = new List<Package>();
  }
}