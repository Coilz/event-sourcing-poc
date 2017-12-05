using System;
using System.Collections.Generic;
using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.EventSourcing.Exceptions;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;
using EventSourcingPoc.Shopping.CommandProcessing;
using EventSourcingPoc.Shopping.Messages.Orders;
using EventSourcingPoc.Shopping.Messages.Shop;

namespace EventSourcingPoc.Shopping.Application
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly Dictionary<Type, Func<IHandler>> _handlerFactories = new Dictionary<Type, Func<IHandler>>();

        public CommandHandlerFactory(Func<IRepository> repositoryProvider)
        {
            RegisterHandlerFactoryWithTypes(
                () => new ShoppingCartCommandHandler(repositoryProvider()),
                typeof(CreateNewCart),
                typeof(AddProductToCart),
                typeof(RemoveProductFromCart),
                typeof(EmptyCart),
                typeof(Checkout));

            RegisterHandlerFactoryWithTypes(
                () => new OrderCommandHandler(repositoryProvider()),
                typeof(PayForOrder),
                typeof(ConfirmShippingAddress),
                typeof(CompleteOrder));
        }

        private void RegisterHandlerFactoryWithTypes(Func<IHandler> handler, params Type[] types)
        {
            foreach(var type in types)
            {
                _handlerFactories.Add(type, handler);
            }
        }

        public ICommandHandler<TCommand> Resolve<TCommand>()
            where TCommand : ICommand
        {
            var commandType = typeof(TCommand);

            if (_handlerFactories.ContainsKey(commandType) &&
                _handlerFactories[commandType]() is ICommandHandler<TCommand> handler)
                return handler;

            throw new NoCommandHandlerRegisteredException(typeof (TCommand));
        }
    }
}
