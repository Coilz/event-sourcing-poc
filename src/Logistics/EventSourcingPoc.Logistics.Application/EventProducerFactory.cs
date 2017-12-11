using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.Kafka;

namespace EventSourcingPoc.Logistics.Application
{
    static class EventProducerFactory
    {
        private static IContextEventProducer _instance;
        public static IContextEventProducer GetEventProducer()
        {
            return _instance ?? (_instance = CreateEventProducer());
        }

        private static IContextEventProducer CreateEventProducer()
        {
            var options = new EventProducerOptions
            {
                Brokers = new string[] {"kafka"} // TODO: get this from appsettings or so
            };
            return new EventProducer(options);
        }
    }
}