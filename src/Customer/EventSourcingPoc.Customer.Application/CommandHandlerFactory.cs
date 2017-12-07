using System;
using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.Customer.CommandProcessing;
using EventSourcingPoc.Customer.Messages.Customer;
using EventSourcingPoc.EventSourcing.Persistence;

namespace EventSourcingPoc.Shopping.Application
{
    public class CommandHandlerFactoryRegistration
    {
        public static CommandHandlerFactory NewCommandHandlerFactory(Func<IRepository> repositoryProvider)
        {
            var commandHandlerFactor = new CommandHandlerFactory();
            commandHandlerFactor.RegisterFactory(
                () => new CustomerCommandHandler(repositoryProvider()),
                typeof(CreateNewCustomer),
                typeof(UpdateCustomer),
                typeof(RemoveCustomer));

            return commandHandlerFactor;
        }
    }
}
