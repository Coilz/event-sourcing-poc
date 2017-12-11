using EventSourcingPoc.EventSourcing.Handlers;
using System;
using EventSourcingPoc.Logistics.Messages.Shipping;
using System.Threading.Tasks;

namespace EventSourcingPoc.Readmodels.Shipping
{
    public class ShipmentEventHandler
        : IEventHandler<ShipmentStarted>
        , IEventHandler<ShipmentDelivered>
    {
        private readonly IShipmentReadModelRepository _repository;

        public ShipmentEventHandler(IShipmentReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(ShipmentStarted @event)
        {
            await ExecuteSaveAsync(@event.AggregateId, model => model.Start());
        }

        public async Task HandleAsync(ShipmentDelivered @event)
        {
            await ExecuteSaveAsync(@event.AggregateId, model => model.Deliver());
        }

        private async Task ExecuteSaveAsync(Guid id, Action<ShipmentReadModel> action)
        {
            var model = await _repository.GetAsync(id);
            action(model);
            await _repository.SaveAsync(model);
        }
    }
}
