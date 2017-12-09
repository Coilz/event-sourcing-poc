using System;
using System.Threading.Tasks;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Shopping.Messages.Shop;
using EventSourcingPoc.Shopping.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using static EventSourcingPoc.Shopping.Application.Bootstrapper;

namespace EventSourcingPoc.Shopping.WebApi.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    public class ShoppingCartController : Controller
    {
        private readonly PretendApplication _app;

        public ShoppingCartController(PretendApplication app)
        {
            _app = app;
        }

        [Route(template: "Customer/{customerId}/ShoppingCart")]
        [HttpGet]
        public async Task<bool> HasCartAsync(Guid customerId)
        {
            return await _app.ShoppingCartReadModelRepository.HasCartAsync(customerId);
        }

        [Route(template: "Customer/{customerId}/ShoppingCart")]
        [HttpPost]
        public async Task CreateNewCart(Guid customerId)
        {
            await _app.SendAsync(new CreateNewCart(Guid.NewGuid(), customerId));
        }

        [Route(template: "ShoppingCart/{cartId}/Product/{productId}")]
        [HttpPost]
        public async Task AddProductToCart(Guid cartId, Guid productId, AddProductToCartDTO dto)
        {
            await _app.SendAsync(new AddProductToCart(cartId, productId, dto.Price));
        }

        [Route(template: "ShoppingCart/{cartId}/Product/{productId}")]
        [HttpDelete]
        public async Task RemoveProductFromCart(Guid cartId, Guid productId)
        {
            await _app.SendAsync(new RemoveProductFromCart(cartId, productId));
        }

        [Route(template: "ShoppingCart/{cartId}/Product")]
        [HttpDelete]
        public async Task EmptyCart(Guid cartId)
        {
            await _app.SendAsync(new EmptyCart(cartId));
        }

        [Route(template: "ShoppingCart/{cartId}/Checkout")]
        [HttpPut]
        public async Task Checkout(Guid cartId)
        {
            await _app.SendAsync(new Checkout(cartId));
        }
    }
}
