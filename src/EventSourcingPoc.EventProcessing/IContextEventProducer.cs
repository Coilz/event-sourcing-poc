using System;
using System.Threading.Tasks;
using EventSourcingPoc.Messages;

namespace EventSourcingPoc.EventProcessing
{
    public interface IContextEventProducer: IDisposable
    {
         Task ProduceAsync<T>(T @event)
            where T : Event;
    }
}
