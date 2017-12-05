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

        public async Task SaveAsync(params EventStream[] streamItems)
        {
            await _repository.SaveAsync(streamItems);
            foreach (var item in streamItems)
            {
                await PublishCommandsAsync((Saga)item);
            }
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
