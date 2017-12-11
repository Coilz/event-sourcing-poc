using System;
using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Logistics.CommandProcessing;
using EventSourcingPoc.Logistics.Messages.Shipment;

namespace EventSourcingPoc.Logistics.Application
{
    public static class CommandHandlerFactoryRegistration
    {
        public static CommandHandlerFactory NewCommandHandlerFactory(Func<IRepository> repositoryProvider)
        {
            var commandHandlerFactor = new CommandHandlerFactory();
            commandHandlerFactor.RegisterFactory(
                () => new ShipmentCommandHandler(repositoryProvider()),
                typeof(CreateShipment),
                typeof(StartShipment),
                typeof(DeliverShipment));

                return commandHandlerFactor;
        }
    }
}
