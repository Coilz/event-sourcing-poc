using System;
using System.Collections.Generic;
using Confluent.Kafka;
using EventSourcingPoc.EventSourcing.Handlers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EventSourcingPoc.Kafka
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IDictionary<string, Type> _messageTypes;
        private readonly ILogger _logger;

        public MessageHandler(IEventDispatcher eventDispatcher, IDictionary<string, Type> messageTypes, ILogger logger)
        {
            _eventDispatcher = eventDispatcher;
            _messageTypes = messageTypes;
            _logger = logger;
        }

        public IEnumerable<string> Topics => _messageTypes.Keys;

        public void OnMessage(object sender, Message<string, string> e)
        {
            _logger.LogInformation($"Handling topic: {e.Topic}, message: {e.Value}");

            var messageType = _messageTypes[e.Topic];
            var evt = JsonConvert.DeserializeObject(e.Value, messageType);

            _logger.LogInformation($"Handling topic: {e.Topic}, type: {evt.GetType()}");

            dynamic typeAwareEvent = evt; //this cast is required to pass the correct Type to the Notify Method. Otherwise IEvent is used as the Type
            _logger.LogInformation($"Handling topic: {e.Topic}, typeAware: {typeAwareEvent.GetType()}");
            _eventDispatcher.SendAsync(typeAwareEvent);
        }
    }
}
