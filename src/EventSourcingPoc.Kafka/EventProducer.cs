using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using EventSourcingPoc.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingPoc.Kafka
{
    public class EventProducer : IDisposable
    {
        private Producer<string, Event> _producer;
        private string _topicName;

        public EventProducer()
        {
            string brokerList = "the first broker;the next broker";
            _topicName = "my special topic";

            var config = new Dictionary<string, object> { { "bootstrap.servers", brokerList } };

            _producer = new Producer<string, Event>(config, new StringSerializer(Encoding.UTF8), null); // TODO: create an EventSerializer
            _producer.OnError += Producer_OnError;
            _producer.OnStatistics += Producer_OnStatistics;
            _producer.OnLog += Producer_OnLog;
        }

        public async Task Produce(Event @event)
        {
            var deliveryReport = await _producer.ProduceAsync(_topicName, null, @event);
            // TODO: log result stuff here
        }

        private void Producer_OnError(object sender, Error e)
        {
            // TODO: log stuff here
        }

        private void Producer_OnLog(object sender, LogMessage e)
        {
            // TODO: log stuff here
        }

        private void Producer_OnStatistics(object sender, string e)
        {
            // TODO: log stuff here
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Tasks are not waited on synchronously (ContinueWith is not synchronous),
                    // so it's possible they may still in progress here.
                    _producer.Flush(TimeSpan.FromSeconds(10));
                    _producer.Dispose();
                    _producer = null;
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
