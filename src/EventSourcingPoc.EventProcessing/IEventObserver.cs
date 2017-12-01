using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventProcessing
{
    public interface IEventObserver
    {
        void Notify<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}