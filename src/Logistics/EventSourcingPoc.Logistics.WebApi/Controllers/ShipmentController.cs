using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static EventSourcingPoc.Logistics.Application.Bootstrapper;

namespace EventSourcingPoc.Logistics.WebApi.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    public class ShipmentController : Controller
    {
        private readonly PretendApplication _app;

        public ShipmentController(PretendApplication app)
        {
            _app = app;
        }

        [Route(template: "Customer/{customerId}/Shipment")]
        [HttpGet]
        public async Task<IEnumerable<Guid>> Shipment(Guid customerId)
        {
            return await _app.ShipmentReadModelRepository.GetShipmentsAsync(customerId);
        }
    }
}
