using System.Threading.Tasks;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Logistics.Messages.Shipping;

namespace EventSourcingPoc.Logistics.EventProcessing
{
    public class ContextEventHandler
        : IEventHandler<ShipmentDelivered>
    {
        private readonly IContextEventProducer _eventProducer;
        public ContextEventHandler(IContextEventProducer eventProducer)
        {
            _eventProducer = eventProducer;
        }

        public async Task HandleAsync(ShipmentDelivered @event)
        {
            await _eventProducer.ProduceAsync(@event);
        }
    }
}
