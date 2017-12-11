using System;
using System.Collections.Generic;
using EventSourcingPoc.EventProcessing;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Kafka;
using EventSourcingPoc.Logistics.Messages.Shipment;

namespace EventSourcingPoc.Logistics.Application
{
    static class EventConsumerFactory
    {
        private static EventConsumer _instance;
        public static EventConsumer GetEventConsumer(IEventDispatcher eventDispatcher)
        {
            return _instance ?? (_instance = CreateEventConsumer(eventDispatcher));
        }

        private static EventConsumer CreateEventConsumer(IEventDispatcher eventDispatcher)
        {
            // TODO: get this from appsettings or so
            var options = new EventConsumerOptions
            {
                Brokers = new string[] {"kafka"},
                AutoCommit = true,
                CommitIntervalMilliseconds = 1000,
                GroupId = "logistics.context",
                StatisticsIntervalMilliseconds = 5000
            };
            return new EventConsumer(CreateMessageHandler(eventDispatcher), options);
        }


        private static IMessageHandler CreateMessageHandler(IEventDispatcher eventDispatcher)
        {
            return new MessageHandler(eventDispatcher, new Dictionary<string, Type>
                {
                    {typeof(ShippingProcessStarted).Name, typeof(ShippingProcessStarted)}
                });
        }
    }
}