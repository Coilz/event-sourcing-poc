using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Application
{
    public interface IDispatcher<in TMessage> where TMessage : ICommand
    {
        void Send(TMessage command);
    }
}