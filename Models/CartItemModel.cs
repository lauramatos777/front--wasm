namespace front__wasm.Models
{
  public class CartItemModel
  {
    public string ImageUrl { get; set; } = "";
    public string ImageAlt { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string UnitPrice { get; set; } = "";
    public int Quantity { get; set; } = 1;
  }
}
