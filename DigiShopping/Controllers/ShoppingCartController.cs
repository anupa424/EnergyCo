using Microsoft.AspNetCore.Mvc;
using DigiShopping.Models;
using System.Collections.Generic;
using System.Linq;
using DigiShopping.Services;

namespace DigiShopping.Controllers
{
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {

        private readonly IShoppingCartBAL _shoppingCartBAL;

        public ShoppingCartController(IShoppingCartBAL shoppingCartBAL)
        {
            _shoppingCartBAL = shoppingCartBAL;
        }


        [Route("[controller]/checkout")]
        [HttpPost]
        public async Task<IActionResult>  CheckOut(ShoppingRequest shoppingRequest)
        {
            var apiResponse = new ApiResponse<ShoppingResponse>();
            try
            {
                apiResponse =await _shoppingCartBAL.Checkout(shoppingRequest);
                return apiResponse.StatusCode switch
                {
                    StatusCodes.Status200OK=>Ok(apiResponse),
                    StatusCodes.Status400BadRequest => BadRequest(apiResponse),
                    StatusCodes.Status401Unauthorized => Unauthorized(apiResponse),


                };
            }
            catch(Exception ex)
            {
                apiResponse.StatusCode = StatusCodes.Status500InternalServerError;
                apiResponse.Errors.Add(ex.Message);
                return BadRequest(apiResponse);

            }

        }

       
       
    }
}
