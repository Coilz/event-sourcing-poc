using Confluent.Kafka;
using Confluent.Kafka.Serialization;
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
        private Consumer<string, Event> _consumer;
        private string _brokerList;
        private List<string> _topics;

        public EventConsumer()
        {
            _topics = new List<string> { "firstTopic", "secondTopic" };

            var config = ConstructConfig(_brokerList, false);
            _consumer = new Consumer<string, Event>(config, new StringDeserializer(Encoding.UTF8), null); // TODO: Create an Event serializer
            _consumer.OnConsumeError += Consumer_OnConsumeError;
            _consumer.OnError += Consumer_OnError;
            _consumer.OnLog += Consumer_OnLog;
            _consumer.OnMessage += Consumer_OnMessage;
            _consumer.OnOffsetsCommitted += Consumer_OnOffsetsCommitted;
            _consumer.OnPartitionEOF += Consumer_OnPartitionEOF;
            _consumer.OnPartitionsAssigned += Consumer_OnPartitionsAssigned;
            _consumer.OnPartitionsRevoked += Consumer_OnPartitionsRevoked;
            _consumer.OnStatistics += Consumer_OnStatistics;
        }

        public bool Consuming { get; private set; }

        public void Start()
        {
            if (Consuming) return;

            Consuming = true;
            _consumer.Subscribe(_topics);

            while (Consuming)
            {
                if (!_consumer.Consume(out Message<string, Event> message, TimeSpan.FromMilliseconds(Timeout)))
                {
                    continue;
                }

                if (message.Offset % 5 == 0)
                {
                    var committedOffsets = _consumer.CommitAsync(message).Result;
                }
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

        private void Consumer_OnMessage(object sender, Message<string, Event> e)
        {
            // TODO: handle the message
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

        private static Dictionary<string, object> ConstructConfig(string brokerList, bool enableAutoCommit) =>
            new Dictionary<string, object>
            {
                { "group.id", "advanced-csharp-consumer" },
                { "enable.auto.commit", enableAutoCommit },
                { "auto.commit.interval.ms", 5000 },
                { "statistics.interval.ms", 60000 },
                { "bootstrap.servers", brokerList },
                { "default.topic.config", new Dictionary<string, object>()
                    {
                        { "auto.offset.reset", "smallest" }
                    }
                }
            };

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
