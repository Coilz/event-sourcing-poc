using System;
using System.Collections.Generic;
using EventSourcingPoc.EventSourcing.Exceptions;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.CommandProcessing
{
    public class CommandHandlerFactory
    {
        private readonly Dictionary<Type, Func<IHandler>> _handlerFactories = new Dictionary<Type, Func<IHandler>>();

        public ICommandHandler<TCommand> ResolveHandler<TCommand>()
            where TCommand : ICommand
        {
            var commandType = typeof(TCommand);

            if (_handlerFactories.ContainsKey(commandType) &&
                _handlerFactories[commandType]() is ICommandHandler<TCommand> handler)
                return handler;

            throw new NoCommandHandlerRegisteredException(typeof (TCommand));
        }

        public void RegisterFactory(Func<IHandler> commandHandler, params Type[] commandTypes)
        {
            foreach(var commandType in commandTypes)
            {
                _handlerFactories.Add(commandType, commandHandler);
            }
        }
    }
}
