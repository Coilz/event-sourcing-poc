using System.Collections.Generic;

namespace EventSourcingPoc.Kafka
{
    public class EventConsumerOptions
    {
        public string GroupId { get; set; }
        public bool AutoCommit { get; set; }
        public int CommitIntervalMilliseconds { get; set; }
        public int StatisticsIntervalMilliseconds { get; set; }
        public string[] Brokers { get; set; }
    }

    static class EventConsumerOptionsExtensions
    {
        public static IDictionary<string, object> ConstructConfig(this EventConsumerOptions options) =>
            new Dictionary<string, object>
            {
                { "group.id", options.GroupId },
                { "enable.auto.commit", options.AutoCommit },
                { "auto.commit.interval.ms", options.CommitIntervalMilliseconds },
                { "statistics.interval.ms", options.StatisticsIntervalMilliseconds },
                { "bootstrap.servers", string.Join(",", options.Brokers)},
                { "default.topic.config", new Dictionary<string, object>()
                    {
                        { "auto.offset.reset", "smallest" }
                    }
                }
            };
    }
}
