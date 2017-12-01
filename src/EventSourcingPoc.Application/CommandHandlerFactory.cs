using System;
using System.Collections.Generic;

namespace EventSourcingPoc.Application
{
    using Domain.Orders;
    using Domain.Store;
    using EventSourcing.Exceptions;
    using EventSourcing.Handlers;
    using EventSourcing.Persistence;
    using Messages;
    using Messages.Orders;
    using Messages.Store;

    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly Dictionary<Type, Func<IHandler>> _handlerFactories = new Dictionary<Type, Func<IHandler>>();

        public CommandHandlerFactory(Func<IRepository> repositoryProvider)
        {
            RegisterHandlerFactoryWithTypes(
                () => new ShoppingCartHandler(repositoryProvider()),
                typeof(CreateNewCart), typeof(AddProductToCart), typeof(RemoveProductFromCart), typeof(EmptyCart), typeof(Checkout));

            RegisterHandlerFactoryWithTypes(
                () => new OrderHandler(repositoryProvider()),
                typeof(PayForOrder), typeof(ConfirmShippingAddress), typeof(CompleteOrder));
        }

        private void RegisterHandlerFactoryWithTypes(Func<IHandler> handler, params Type[] types)
        {
            foreach(var type in types)
            {
                _handlerFactories.Add(type, handler);
            }
        }

        public ICommandHandler<TCommand> Resolve<TCommand>() where TCommand : ICommand
        {
            var commandType = typeof(TCommand);

            if (_handlerFactories.ContainsKey(commandType) &&
                _handlerFactories[commandType]() is ICommandHandler<TCommand> handler)
                return handler;

            throw new NoCommandHandlerRegisteredException(typeof (TCommand));
        }
    }
}
