using System.Threading.Tasks;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Logistics.Domain.Shipping;
using EventSourcingPoc.Logistics.Messages.Orders;
using EventSourcingPoc.Logistics.Messages.Shipping;

namespace EventSourcingPoc.Logistics.EventProcessing
{
    public class ContextEventHandler
        : IEventHandler<ShippingProcessStarted>
    {
        private readonly IContextEventProducer _eventProducer;
        public ContextEventHandler(IContextEventProducer eventProducer)
        {
            _eventProducer = eventProducer;
        }

        public async Task HandleAsync(ShippingProcessStarted @event)
        {
            await _eventProducer.ProduceAsync(@event);
        }
    }
}
