using System;
using System.Collections.Generic;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.Kafka;
using EventSourcingPoc.Shopping.Messages.Shipping;
using Microsoft.Extensions.Logging;

namespace EventSourcingPoc.Shopping.Application
{
    static class EventProducerFactory
    {
        private static IContextEventProducer _instance;
        public static IContextEventProducer GetEventProducer(ILogger logger)
        {
            return _instance ?? (_instance = CreateEventProducer(logger));
        }

        private static IContextEventProducer CreateEventProducer(ILogger logger)
        {
            var options = new EventProducerOptions
            {
                Brokers = new string[] {"kafka"} // TODO: get this from appsettings or so
            };
            return new EventProducer(options, GetMessageTopics(), logger);
        }

        private static IDictionary<Type, string> GetMessageTopics()
        {
            return new Dictionary<Type, string>
            {
                {typeof(ShippingProcessStarted), "shipping-started"}
            };
        }
    }
}