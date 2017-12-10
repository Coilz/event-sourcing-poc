using System.Collections.Generic;
using Confluent.Kafka;

namespace EventSourcingPoc.Kafka
{
    public interface IMessageHandler
    {
        IEnumerable<string> Topics {get;}
        void OnMessage(object sender, Message<string, string> e);
    }
}