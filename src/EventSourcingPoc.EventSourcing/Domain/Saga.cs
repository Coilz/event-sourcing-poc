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

        protected void QueueCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public IEnumerable<ICommand> GetUndispatchedCommands() => _commands.AsReadOnly();

        public void ClearUndispatchedCommands()
        {
            _commands = new List<ICommand>();
        }
    }
}
