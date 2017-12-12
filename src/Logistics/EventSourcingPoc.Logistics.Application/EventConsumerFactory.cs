using System;
using System.Collections.Generic;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Kafka;
using EventSourcingPoc.Logistics.Messages.Shipment;
using Microsoft.Extensions.Logging;

namespace EventSourcingPoc.Logistics.Application
{
    static class EventConsumerFactory
    {
        private static EventConsumer _instance;
        public static EventConsumer GetEventConsumer(IEventDispatcher eventDispatcher, ILogger logger)
        {
            return _instance ?? (_instance = CreateEventConsumer(eventDispatcher, logger));
        }

        private static EventConsumer CreateEventConsumer(IEventDispatcher eventDispatcher, ILogger logger)
        {
            // TODO: get this from appsettings or so
            var options = new EventConsumerOptions
            {
                Brokers = new string[] {"kafka"},
                AutoCommit = true,
                CommitIntervalMilliseconds = 5000,
                GroupId = "logistics-context",
                StatisticsIntervalMilliseconds = 60000
            };
            return new EventConsumer(CreateMessageHandler(eventDispatcher, logger), options, logger);
        }


        private static IMessageHandler CreateMessageHandler(IEventDispatcher eventDispatcher, ILogger logger)
        {
            return new MessageHandler(eventDispatcher, new Dictionary<string, Type>
                {
                    {"shipping-started", typeof(ShippingProcessStarted)}
                },
                logger);
        }
    }
}