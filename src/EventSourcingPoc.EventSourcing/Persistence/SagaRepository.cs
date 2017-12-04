using System;
using System.Linq;
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

        public T GetById<T>(Guid id)
            where T : EventStream, new()
        {
            return _repository.GetById<T>(id);
        }

        public void Save(params EventStream[] streamItems)
        {
            _repository.Save(streamItems);
            foreach (var item in streamItems)
            {
                PublishCommands((Saga)item);
            }
        }

        private void PublishCommands(Saga saga)
        {
            foreach (var command in saga.GetUndispatchedCommands())
            {
                dynamic typeAwareCommand = command; //this cast is required to pass the correct Type to the Notify Method. Otherwise IEvent is used as the Type
                _commandDispatcher.Send(typeAwareCommand);
            }
            saga.ClearUndispatchedCommands();
        }
    }
}
