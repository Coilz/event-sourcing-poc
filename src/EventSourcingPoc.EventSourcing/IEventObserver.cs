using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventSourcing
{
    public interface IEventObserver
    {
        void Notify<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}