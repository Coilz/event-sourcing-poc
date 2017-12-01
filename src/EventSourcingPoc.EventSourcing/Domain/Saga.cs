using System.Collections.Generic;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing.Domain
{
    public abstract class Saga : EventStream
    {
        private readonly List<ICommand> _unpublishedCommands;

        protected Saga()
        {
            _unpublishedCommands = new List<ICommand>();
        }

        protected void Publish(ICommand command)
        {
            _unpublishedCommands.Add(command);
        }
    }
}
