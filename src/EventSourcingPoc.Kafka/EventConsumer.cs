using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using EventSourcingPoc.EventSourcing.Handlers;
using EventSourcingPoc.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingPoc.Kafka
{
    public class EventConsumer : IDisposable
    {
        private const int Timeout = 100;
        private readonly IMessageHandler _messageHandler;
        private Consumer<string, string> _consumer;

        public EventConsumer(IMessageHandler messageHandler, EventConsumerOptions options)
        {
            _messageHandler = messageHandler;

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

            while (Consuming)
            {
                _consumer.Poll(TimeSpan.FromMilliseconds(Timeout));
            }
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
            // TODO: log stuff here
        }

        private void Consumer_OnPartitionsRevoked(object sender, List<TopicPartition> e)
        {
            // TODO: log stuff here
        }

        private void Consumer_OnPartitionsAssigned(object sender, List<TopicPartition> e)
        {
            // TODO: log stuff here
        }

        private void Consumer_OnPartitionEOF(object sender, TopicPartitionOffset e)
        {
            // TODO: log stuff here
        }

        private void Consumer_OnOffsetsCommitted(object sender, CommittedOffsets e)
        {
            // TODO: log stuff here
        }

        private void Consumer_OnLog(object sender, LogMessage e)
        {
            // TODO: log stuff here
        }

        private void Consumer_OnError(object sender, Error e)
        {
            // TODO: log stuff here
        }

        private void Consumer_OnConsumeError(object sender, Message e)
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
