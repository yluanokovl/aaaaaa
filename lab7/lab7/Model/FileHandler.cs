namespace lab7.Model
{
    public class FileHandler : EventHandlerBase
    {
        IFormatStrategy _strategy;
        public FileHandler(IFormatStrategy strategy) : base(strategy)
        {
            _strategy = strategy;
        }

        public override string FormatMessage(string msg, object data)
        {
            if (data is MetricData metricData)
            {
                return _strategy.Format(msg, metricData.Timestamp);
            }
            return _strategy.Format(msg, DateTime.Now);
        }

        public override void SendMessage(string msg)
        {
            Console.WriteLine($"четотам filewrite {msg}");
        }

        public override void LogResult()
        {
            Console.WriteLine($"very cool file log at {DateTime.Now}");
        }
    }
}
