using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventProcessing
{
    public interface IEventObserver // TODO: move to EventSourcing
    {
        void Notify<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}