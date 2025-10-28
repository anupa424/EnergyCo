using DigiShopping.Models;

namespace DigiShopping.Services
{
    public interface IShoppingCartBAL
    {

        public Task<ApiResponse<ShoppingResponse>> Checkout(ShoppingRequest shoppingRequest);
    }
}