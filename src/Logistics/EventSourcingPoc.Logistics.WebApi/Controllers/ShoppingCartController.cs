using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [Route(template: "Customer/{customerId}/Shipments")]
        [HttpGet]
        public async Task<IEnumerable<Guid>> GetShioments(Guid customerId)
        {
            return await _app.ShipmentReadModelRepository.GetShipmentsAsync(customerId);
        }
    }
}
