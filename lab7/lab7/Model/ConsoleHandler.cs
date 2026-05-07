using System;
using System.Collections.Generic;
using System.Text;

namespace lab7.Model
{
    public class ConsoleHandler: EventHandlerBase
    {
        IFormatStrategy _strategy;
        public ConsoleHandler(IFormatStrategy strategy) : base(strategy) 
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
            Console.WriteLine(msg);
        }

        public override void LogResult()
        {
            Console.WriteLine($"very cool console log at {DateTime.Now}");
        }
    }
}
