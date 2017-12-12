using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using EventSourcingPoc.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EventSourcingPoc.EventProcessing;
using Microsoft.Extensions.Logging;

namespace EventSourcingPoc.Kafka
{
    public class EventProducer : IContextEventProducer
    {
        private readonly IDictionary<Type, string> _messageTopics;
        private Producer<string, string> _producer;
        private readonly ILogger _logger;

        public EventProducer(EventProducerOptions options, IDictionary<Type, string> messageTopics, ILogger logger)
        {
            _messageTopics = messageTopics;
            _logger = logger;

            _producer = new Producer<string, string>(
                options.ConstructConfig(),
                new StringSerializer(Encoding.UTF8),
                new StringSerializer(Encoding.UTF8));

            _producer.OnError += Producer_OnError;
            _producer.OnStatistics += Producer_OnStatistics;
            _producer.OnLog += Producer_OnLog;
        }

        public async Task ProduceAsync<T>(T @event)
            where T : Event
        {
            string json = JsonConvert.SerializeObject(@event);
            var topic = _messageTopics[typeof(T)];
            var deliveryReport = await _producer.ProduceAsync(topic, @event.AggregateId.ToString(), json);

            _logger.LogInformation($"Produced event on topic: {deliveryReport.Topic}, value {deliveryReport.Value}");
        }

        private void Producer_OnError(object sender, Error e)
        {
            _logger.LogError($"Produced error reason: {e.Reason}");
        }

        private void Producer_OnLog(object sender, LogMessage e)
        {
            _logger.LogInformation($"Produced log message: {e.Message}");
        }

        private void Producer_OnStatistics(object sender, string e)
        {
            _logger.LogInformation($"Produced statistics: {e}");
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
