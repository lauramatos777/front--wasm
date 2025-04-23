using Microsoft.JSInterop;
using System.Text.Json;
using front__wasm.Models;

namespace front__wasm.Services
{
    public class CartService
    {
        private List<CartItemModel> _cartItems = new List<CartItemModel>();
        private readonly IJSRuntime? _jsRuntime;
        private bool _initialized = false;

        public event Action? OnChange;

        public IReadOnlyList<CartItemModel> CartItems => _cartItems.AsReadOnly();

        public int Count => _cartItems.Sum(item => item.Quantity);

        public CartService(IJSRuntime? jsRuntime = null)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            if (_initialized)
                return;

            if (_jsRuntime != null)
            {
                try
                {
                    var storedCart = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "cartItems");

                    if (!string.IsNullOrEmpty(storedCart))
                    {
                        var savedItems = JsonSerializer.Deserialize<List<CartItemModel>>(storedCart);
                        if (savedItems != null && savedItems.Any())
                        {
                            _cartItems = savedItems;
                            NotifyStateChanged();
                        }
                    }
                }
                catch
                {
                    // No action needed when storage access fails
                }
            }

            _initialized = true;
        }

        public void AddItem(CartItemModel item)
        {
            var existingItem = _cartItems.FirstOrDefault(i =>
                i.Title == item.Title &&
                i.Description == item.Description);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                _cartItems.Add(item);
            }

            SaveCartToStorage();
            NotifyStateChanged();
        }

        public void UpdateQuantity(CartItemModel item, int newQuantity)
        {
            var existingItem = _cartItems.FirstOrDefault(i => i == item);
            if (existingItem != null)
            {
                existingItem.Quantity = newQuantity;
                SaveCartToStorage();
                NotifyStateChanged();
            }
        }

        public void RemoveItem(CartItemModel item)
        {
            _cartItems.Remove(item);
            SaveCartToStorage();
            NotifyStateChanged();
        }

        public void ClearCart()
        {
            _cartItems.Clear();
            SaveCartToStorage();
            NotifyStateChanged();
        }

        private async void SaveCartToStorage()
        {
            if (_jsRuntime != null)
            {
                try
                {
                    var cartJson = JsonSerializer.Serialize(_cartItems);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "cartItems", cartJson);
                }
                catch
                { }
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}