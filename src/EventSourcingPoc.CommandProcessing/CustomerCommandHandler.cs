using EventSourcingPoc.Domain.Customers;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages.Customers;

namespace EventSourcingPoc.CommandProcessing
{
    public class CustomerCommandHandler
        : CommandHandler<Customer>
        , ICommandHandler<CreateNewCustomer>
        , ICommandHandler<UpdateCustomer>
        , ICommandHandler<RemoveCustomer>
    {
        public CustomerCommandHandler(IRepository repository)
            : base(repository)
        {}

        public void Handle(CreateNewCustomer command)
        {
            Repository.Save(Customer.Create(command.CustomerId, command.Info));
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
