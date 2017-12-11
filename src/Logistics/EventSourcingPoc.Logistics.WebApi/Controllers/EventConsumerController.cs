using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static EventSourcingPoc.Logistics.Application.Bootstrapper;

namespace EventSourcingPoc.Logistics.WebApi.Controllers
{
    [Route("api/EventConsumer")]
    [Produces("application/json")]
    public class EventConsumerController : Controller
    {
        private readonly PretendApplication _app;

        public EventConsumerController(PretendApplication app)
        {
            _app = app;
        }

        [Route("Start")]
        [HttpPost]
        public void Start()
        {
            _app.EventConsumer.Start();
        }

        [Route("Stop")]
        [HttpPost]
        public void Stop()
        {
            _app.EventConsumer.Stop();
        }
    }
}
