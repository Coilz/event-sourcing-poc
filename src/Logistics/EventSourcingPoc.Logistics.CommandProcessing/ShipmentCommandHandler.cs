using System;
using System.Threading.Tasks;
using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Logistics.Domain.Shipping;
using EventSourcingPoc.Logistics.Messages.Shipment;

namespace EventSourcingPoc.Logistics.CommandProcessing
{
    public class ShipmentCommandHandler
        : CommandHandler<Shipment>
        , ICommandHandler<CreateShipment>
        , ICommandHandler<StartShipment>
        , ICommandHandler<DeliverShipment>
    {
        public ShipmentCommandHandler(IRepository repository)
            : base(repository)
        {}

        public async Task HandleAsync(StartShipment command)
        {
            await ExecuteAsync(command.AggregateId, aggregate => aggregate.Start());
        }

        public async Task HandleAsync(DeliverShipment command)
        {
            await ExecuteAsync(command.AggregateId, aggregate => aggregate.Deliver());
        }

        public async Task HandleAsync(CreateShipment command)
        {
            await Repository.SaveAsync(Shipment.Create(Guid.NewGuid(), command.CustomerId, command.ShippingItems));
        }
    }
}
