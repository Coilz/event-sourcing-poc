using System.Threading.Tasks;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Shopping.Domain.Shipping;
using EventSourcingPoc.Shopping.Messages.Orders;
using EventSourcingPoc.Shopping.Messages.Shipping;

namespace EventSourcingPoc.Shopping.EventProcessing
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
