using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Logistics.Domain.Shipping;

namespace EventSourcingPoc.Logistics.EventProcessing
{
    public class ShipmentEventHandler
        : EventHandler<Shipment>
    {
        public ShipmentEventHandler(IRepository repository)
            : base(repository)
        {
        }
    }
}
