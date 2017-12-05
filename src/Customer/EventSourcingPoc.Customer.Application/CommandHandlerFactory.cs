using System;
using System.Collections.Generic;
using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.Customer.CommandProcessing;
using EventSourcingPoc.Customer.Messages.Customer;
using EventSourcingPoc.EventSourcing.Exceptions;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Shopping.Application
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly Dictionary<Type, Func<IHandler>> _handlerFactories = new Dictionary<Type, Func<IHandler>>();

        public CommandHandlerFactory(Func<IRepository> repositoryProvider)
        {
            RegisterHandlerFactoryWithTypes(
                () => new CustomerCommandHandler(repositoryProvider()),
                typeof(CreateNewCustomer),
                typeof(UpdateCustomer),
                typeof(RemoveCustomer));
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
