using System.Threading.Tasks;
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

        public async Task HandleAsync(CreateNewCustomer command)
        {
            await Repository.SaveAsync(Domain.Customers.Customer.Create(command.CustomerId, command.Info));
        }

        public async Task HandleAsync(UpdateCustomer command)
        {
            await ExecuteAsync(command.CustomerId, model => model.Update(command.Info));
        }

        public async Task HandleAsync(RemoveCustomer command)
        {
            await ExecuteAsync(command.CustomerId, model => model.Remove());
        }
    }
}
