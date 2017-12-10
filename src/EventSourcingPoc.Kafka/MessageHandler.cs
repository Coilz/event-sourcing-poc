using System;
using System.Collections.Generic;
using Confluent.Kafka;
using EventSourcingPoc.EventSourcing.Handlers;
using Newtonsoft.Json;

namespace EventSourcingPoc.Kafka
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IDictionary<string, Type> _messageTypes;

        public MessageHandler(IEventDispatcher eventDispatcher, IDictionary<string, Type> messageTypes)
        {
            _eventDispatcher = eventDispatcher;
            _messageTypes = messageTypes;
        }

        public IEnumerable<string> Topics => _messageTypes.Keys;

        public void OnMessage(object sender, Message<string, string> e)
        {
            var messageType = _messageTypes[e.Topic];
            dynamic @event = JsonConvert.DeserializeObject(e.Value, messageType);

            dynamic typeAwareEvent = @event; //this cast is required to pass the correct Type to the Notify Method. Otherwise IEvent is used as the Type
            _eventDispatcher.SendAsync(typeAwareEvent);
        }
    }
}
