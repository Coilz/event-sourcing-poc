using System;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingPoc.EventSourcing.Domain;
using EventSourcingPoc.EventSourcing.Handlers;

namespace EventSourcingPoc.EventSourcing.Persistence
{
    public class SagaRepository : IRepository
    {
        private readonly IRepository _repository;
        private readonly ICommandDispatcher _commandDispatcher;

        public SagaRepository(IRepository repository, ICommandDispatcher commandDispatcher)
        {
            _repository = repository;
            _commandDispatcher = commandDispatcher;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
            where T : EventStream, new()
        {
            return await _repository.GetByIdAsync<T>(id);
        }

        public async Task SaveAsync(EventStream streamItem)
        {
            await _repository.SaveAsync(streamItem);
            await PublishCommandsAsync((Saga)streamItem);
        }

        private async Task PublishCommandsAsync(Saga saga)
        {
            var messages = saga.GetUndispatchedCommands().Select(command => {
                dynamic typeAwareCommand = command; //this cast is required to pass the correct Type to the Notify Method. Otherwise IEvent is used as the Type
                return (Task)_commandDispatcher.SendAsync(typeAwareCommand);
            });
            await Task.WhenAll(messages);

            saga.ClearUndispatchedCommands();
        }
    }
}
