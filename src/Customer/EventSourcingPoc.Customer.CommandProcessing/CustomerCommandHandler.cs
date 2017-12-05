using EventSourcingPoc.CommandProcessing;
using EventSourcingPoc.Customer.Messages.Customer;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;

namespace EventSourcingPoc.Customer.CommandProcessing
{
    public class CustomerCommandHandler
        : CommandHandler<Domain.Customers.Customer>
        , ICommandHandler<CreateNewCustomer>
        , ICommandHandler<UpdateCustomer>
        , ICommandHandler<RemoveCustomer>
    {
        public CustomerCommandHandler(IRepository repository)
            : base((IRepository) repository)
        {}

        public void Handle(CreateNewCustomer command)
        {
            Repository.Save(Domain.Customers.Customer.Create(command.CustomerId, command.Info));
        }

        public void Handle(UpdateCustomer command)
        {
            Execute(command.CustomerId, model => model.Update(command.Info));
        }

        public void Handle(RemoveCustomer command)
        {
            Execute(command.CustomerId, model => model.Remove());
        }
    }
}
