using System.Collections.Generic;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Domain
{
    public abstract class Saga : EventStream
    {
        private List<ICommand> _commands;

        protected Saga()
        {
            _commands = new List<ICommand>();
        }

        protected void Queue(ICommand command)
        {
            _commands.Add(command);
        }

        public IEnumerable<ICommand> GetUndispatchedMessages() => _commands.AsReadOnly();

        public void ClearUndispatchedMessages()
        {
            _commands = new List<ICommand>();
        }
    }
}
