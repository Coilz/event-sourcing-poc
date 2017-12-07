using System;
using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Shopping.CommandProcessing;
using EventSourcingPoc.Shopping.Messages.Orders;
using EventSourcingPoc.Shopping.Messages.Shop;

namespace EventSourcingPoc.Shopping.Application
{
    public static class CommandHandlerFactoryRegistration
    {
        public static CommandHandlerFactory NewCommandHandlerFactory(Func<IRepository> repositoryProvider)
        {
            var commandHandlerFactor = new CommandHandlerFactory();
            commandHandlerFactor.RegisterFactory(
                () => new ShoppingCartCommandHandler(repositoryProvider()),
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
