using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EventSourcingPoc.Kafka
{
    public class EventConsumer : IDisposable
    {
        private const int Timeout = 1000;
        private readonly IMessageHandler _messageHandler;
        private Consumer<string, string> _consumer;
        private readonly ILogger _logger;

        public EventConsumer(IMessageHandler messageHandler, EventConsumerOptions options, ILogger logger)
        {
            _messageHandler = messageHandler;
            _logger = logger;

            _consumer = new Consumer<string, string>(
                options.ConstructConfig(),
                new StringDeserializer(Encoding.UTF8),
                new StringDeserializer(Encoding.UTF8));

            _consumer.OnMessage += _messageHandler.OnMessage;

            _consumer.OnStatistics += Consumer_OnStatistics;
            _consumer.OnPartitionsRevoked += Consumer_OnPartitionsRevoked;
            _consumer.OnPartitionsAssigned += Consumer_OnPartitionsAssigned;
            _consumer.OnPartitionEOF += Consumer_OnPartitionEOF;
            _consumer.OnOffsetsCommitted += Consumer_OnOffsetsCommitted;
            _consumer.OnLog += Consumer_OnLog;
            _consumer.OnError += Consumer_OnError;
            _consumer.OnConsumeError += Consumer_OnConsumeError;
        }

        public bool Consuming { get; private set; }

        public void Start()
        {
            if (Consuming) return;

            Consuming = true;
            _consumer.Subscribe(_messageHandler.Topics);
            _logger.LogInformation($"Subscribed to: [{string.Join(", ", _consumer.Subscription)}]");

            Task.Run(() => {
                while (Consuming)
                {
                    _consumer.Poll(TimeSpan.FromMilliseconds(Timeout));
                }
            });
        }

        public void Stop()
        {
            if (!Consuming) return;

            Consuming = false;
            _consumer.Unsubscribe();

            Task.Delay(TimeSpan.FromMilliseconds(Timeout)).GetAwaiter().GetResult();
        }

        private void Consumer_OnStatistics(object sender, string e)
        {
            _logger.LogInformation($"Consumer statistics: {e}");
        }

        private void Consumer_OnPartitionsRevoked(object sender, List<TopicPartition> e)
        {
            foreach (var topicPartition in e)
            {
                _logger.LogInformation($"Consumer partitions revoked, topic: {topicPartition.Topic}, {topicPartition}");
            }
            _consumer.Unassign();
        }

        private void Consumer_OnPartitionsAssigned(object sender, List<TopicPartition> e)
        {
            foreach (var topicPartition in e)
            {
                _logger.LogInformation($"Consumer partitions assigned, topic: {topicPartition.Topic}, {topicPartition}");
            }
            _consumer.Assign(e);
        }

        private void Consumer_OnPartitionEOF(object sender, TopicPartitionOffset e)
        {
            _logger.LogInformation($"Consumer PartitionEOF, topic: {e.Topic}, {e}");
        }

        private void Consumer_OnOffsetsCommitted(object sender, CommittedOffsets e)
        {
            _logger.LogInformation($"Consumer offsets [{string.Join(", ", e.Offsets)}]");

            if (e.Error)
            {
                _logger.LogInformation($"Consumer failed to commit offsets: {e.Error}");
            }
            _logger.LogInformation($"Consumer successfully committed offsets: [{string.Join(", ", e.Offsets)}]");
        }

        private void Consumer_OnLog(object sender, LogMessage e)
        {
            _logger.LogInformation($"Consumer log, message: {e.Message}, {e}");
        }

        private void Consumer_OnError(object sender, Error e)
        {
            _logger.LogError($"Consumer error, reason: {e.Reason}, {e}");
        }

        private void Consumer_OnConsumeError(object sender, Message e)
        {
            _logger.LogError($"Consumer error, topic: {e.Topic}, {e}");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Stop();
                    _consumer.Dispose();
                    _consumer = null;
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
