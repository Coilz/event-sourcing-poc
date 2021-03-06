using System.Collections.Generic;

namespace EventSourcingPoc.Kafka
{
    public class EventProducerOptions
    {
        public string[] Brokers { get; set; }
        public int StatisticsIntervalMilliseconds { get; set; }
    }

    static class EventProducerOptionsExtensions
    {
        public static IDictionary<string, object> ConstructConfig(this EventProducerOptions options) =>
            new Dictionary<string, object>
            {
                { "bootstrap.servers", string.Join(",", options.Brokers)},
                { "statistics.interval.ms", options.StatisticsIntervalMilliseconds },
            };
    }
}
