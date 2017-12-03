using System;
using EventSourcingPoc.Domain.Customers;
using EventSourcingPoc.Domain.Orders;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.EventSourcing.Persistence;
using EventSourcingPoc.Messages.Customers;
using EventSourcingPoc.Messages.Orders;

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

        public void Handle(CreateNewCustomer cmd)
        {
            Repository.Save(Customer.Create(cmd.CustomerId, cmd.Info));
        }

        public void Handle(UpdateCustomer cmd)
        {
            Execute(cmd.CustomerId, model => model.Update(cmd.Info));
        }

        public void Handle(RemoveCustomer cmd)
        {
            Execute(cmd.CustomerId, model => model.Remove());
        }
    }
}
