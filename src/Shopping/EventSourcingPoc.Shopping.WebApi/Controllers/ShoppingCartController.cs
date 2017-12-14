using System;
using System.Collections.Generic;
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
        public async Task<IEnumerable<Guid>> GetCarts(Guid customerId)
        {
            return await _app.ShoppingCartReadModelRepository.GetCartsAsync(customerId);
        }

        [Route(template: "Customer/{customerId}/ShoppingCart")]
        [HttpPost]
        public async Task CreateNewCart(Guid customerId)
        {
            await _app.SendAsync(new CreateNewCart(Guid.NewGuid(), customerId));
        }

        [Route(template: "ShoppingCart/{id}/Product/{productId}")]
        [HttpPost]
        public async Task AddProductToCart(Guid id, Guid productId, [FromBody] AddProductToCartDTO dto)
        {
            await _app.SendAsync(new AddProductToCart(id, productId, dto.Price));
        }

        [Route(template: "ShoppingCart/{id}/Product/{productId}")]
        [HttpDelete]
        public async Task RemoveProductFromCart(Guid id, Guid productId)
        {
            await _app.SendAsync(new RemoveProductFromCart(id, productId));
        }

        [Route(template: "ShoppingCart/{id}/Product")]
        [HttpDelete]
        public async Task EmptyCart(Guid id)
        {
            await _app.SendAsync(new EmptyCart(id));
        }

        [Route(template: "ShoppingCart/{id}/Checkout")]
        [HttpPut]
        public async Task Checkout(Guid id)
        {
            await _app.SendAsync(new Checkout(id));
        }
    }
}
