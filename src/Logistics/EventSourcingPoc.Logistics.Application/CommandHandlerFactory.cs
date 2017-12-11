using System;
using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Logistics.CommandProcessing;
using EventSourcingPoc.Logistics.Messages.Orders;
using EventSourcingPoc.Logistics.Messages.Shop;

namespace EventSourcingPoc.Logistics.Application
{
    public static class CommandHandlerFactoryRegistration
    {
        public static CommandHandlerFactory NewCommandHandlerFactory(Func<IRepository> repositoryProvider)
        {
            var commandHandlerFactor = new CommandHandlerFactory();
            commandHandlerFactor.RegisterFactory(
                () => new LogisticsCartCommandHandler(repositoryProvider()),
                typeof(CreateNewCart),
                typeof(AddProductToCart),
                typeof(RemoveProductFromCart),
                typeof(EmptyCart),
                typeof(Checkout));

            commandHandlerFactor.RegisterFactory(
                () => new OrderCommandHandler(repositoryProvider()),
                typeof(PayForOrder),
                typeof(ConfirmShippingAddress),
                typeof(CompleteOrder));

                return commandHandlerFactor;
        }
    }
}
