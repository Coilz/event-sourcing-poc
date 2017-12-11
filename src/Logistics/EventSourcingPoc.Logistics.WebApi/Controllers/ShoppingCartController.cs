using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Logistics.Messages.Shop;
using EventSourcingPoc.Logistics.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using static EventSourcingPoc.Logistics.Application.Bootstrapper;

namespace EventSourcingPoc.Logistics.WebApi.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    public class LogisticsCartController : Controller
    {
        private readonly PretendApplication _app;

        public LogisticsCartController(PretendApplication app)
        {
            _app = app;
        }

        [Route(template: "Customer/{customerId}/LogisticsCart")]
        [HttpGet]
        public async Task<IEnumerable<Guid>> GetCarts(Guid customerId)
        {
            return await _app.LogisticsCartReadModelRepository.GetCartsAsync(customerId);
        }

        [Route(template: "Customer/{customerId}/LogisticsCart")]
        [HttpPost]
        public async Task CreateNewCart(Guid customerId)
        {
            await _app.SendAsync(new CreateNewCart(Guid.NewGuid(), customerId));
        }

        [Route(template: "LogisticsCart/{cartId}/Product/{productId}")]
        [HttpPost]
        public async Task AddProductToCart(Guid cartId, Guid productId, AddProductToCartDTO dto)
        {
            await _app.SendAsync(new AddProductToCart(cartId, productId, dto.Price));
        }

        [Route(template: "LogisticsCart/{cartId}/Product/{productId}")]
        [HttpDelete]
        public async Task RemoveProductFromCart(Guid cartId, Guid productId)
        {
            await _app.SendAsync(new RemoveProductFromCart(cartId, productId));
        }

        [Route(template: "LogisticsCart/{cartId}/Product")]
        [HttpDelete]
        public async Task EmptyCart(Guid cartId)
        {
            await _app.SendAsync(new EmptyCart(cartId));
        }

        [Route(template: "LogisticsCart/{cartId}/Checkout")]
        [HttpPut]
        public async Task Checkout(Guid cartId)
        {
            await _app.SendAsync(new Checkout(cartId));
        }
    }
}
