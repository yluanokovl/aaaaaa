using System;
using System.Collections.Generic;
using System.Text;

namespace lab7.Model
{
    public delegate void MetricEventHandler(MetricEventArgs e);

    public class EventMonitor
    {

        // Событие (Event) — это ключевой элемент паттерна Observer в C#
        public event MetricEventHandler? OnMetricExceeded;
        public void CheckMetric(string metricName = "swag", double value = 17171717, double threshold = 701114)
        {
            Console.WriteLine($"[Monitor]: Checking {metricName} ({value} vs {threshold})");
            if (value > threshold)
            {
                // Вместо цикла foreach в методе Notify, мы просто вызываем событие.
                // ?.Invoke проверяет, есть ли подписчики.
                MetricData eventData = new MetricData(metricName, value, threshold, DateTime.Now);
                OnMetricExceeded?.Invoke(new MetricEventArgs(eventType: metricName +
                "_Exceeded", data: eventData));
            }
        }
    }
}
