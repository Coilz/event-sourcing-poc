using EventSourcingPoc.Messages;

namespace EventSourcingPoc.Data
{
    public interface IEventObserver
    {
        void Notify<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}