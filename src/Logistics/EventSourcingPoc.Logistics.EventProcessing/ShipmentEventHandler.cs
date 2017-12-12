using System.Threading.Tasks;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Logistics.Domain.Shipping;
using EventSourcingPoc.Logistics.Messages.Shipment;

namespace EventSourcingPoc.Logistics.EventProcessing
{
    public class ShipmentEventHandler
        : EventHandler<Shipment>
        , IEventHandler<ShippingProcessStarted>
   {
        public ShipmentEventHandler(IRepository repository)
            : base(repository)
        {
        }

        public async Task HandleAsync(ShippingProcessStarted @event)
        {
            var shipment = Shipment.Create(@event.AggregateId, @event.CustomerId, @event.Items);
            await Repository.SaveAsync(shipment);
        }
    }
}
