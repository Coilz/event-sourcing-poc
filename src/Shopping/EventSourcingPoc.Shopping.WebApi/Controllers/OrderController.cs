using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Readmodels.Orders;
using EventSourcingPoc.Shopping.Messages.Orders;
using EventSourcingPoc.Shopping.Messages.Shop;
using EventSourcingPoc.Shopping.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using static EventSourcingPoc.Shopping.Application.Bootstrapper;

namespace EventSourcingPoc.Shopping.WebApi.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    public class OrderController : Controller
    {
        private readonly PretendApplication _app;

        public OrderController(PretendApplication app)
        {
            _app = app;
        }

        [Route(template: "Customer/{customerId}/Order")]
        [HttpGet]
        public async Task<IEnumerable<Guid>> GetOrdersAsync(Guid customerId)
        {
            return await _app.OrderReadModelRepository.GetOrdersAsync(customerId);
        }

        [Route(template: "Order/{id}")]
        [HttpGet]
        public async Task<OrderReadModel> GetOrderAsync(Guid id)
        {
            return await _app.OrderReadModelRepository.GetAsync(id);
        }

        [Route(template: "Order/{id}/Pay")]
        [HttpPut]
        public async Task PayForOrderAsync(Guid id)
        {
            await _app.SendAsync(new PayForOrder(id)); // TODO: This should be an event from a payment service
        }

        [Route(template: "Order/{id}/ShippingAddress")]
        [HttpPut]
        public async Task ProvideShippingAddressAsync(Guid id, [FromBody] ShippingAddressDTO address)
        {
            await _app.SendAsync(new ProvideShippingAddress(id, new Address(address.Address)));
        }
    }
}
