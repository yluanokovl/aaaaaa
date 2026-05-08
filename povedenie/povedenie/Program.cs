// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

// 1. Класс события
public class Event
{
    public string Timestamp { get; }
    public string Metric { get; }
    public object Value { get; }

    public Event(string metric, object value)
    {
        Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Metric = metric;
        Value = value;
    }
}

// 2. Интерфейс стратегии форматирования сообщений
public interface IMessagingStrategy
{
    string FormatMessage(Event evt);
}

// 3. Реализации стратегий
public class TextMessagingStrategy : IMessagingStrategy
{
    public string FormatMessage(Event evt)
    {
        return $"Критическое событие:\nВремя: {evt.Timestamp}\nМетрика: {evt.Metric}\nЗначение: {evt.Value}";
    }
}
//

public class JsonMessagingStrategy : IMessagingStrategy
{
    public string FormatMessage(Event evt)
    {
        var dict = new Dictionary<string, object>
        {
            {"timestamp", evt.Timestamp },
            {"metric", evt.Metric },
            {"value", evt.Value }
        };
        return JsonSerializer.Serialize(dict);
    }
}

public class HtmlMessagingStrategy : IMessagingStrategy
{
    public string FormatMessage(Event evt)
    {
        return $"<html><body><h2>Критическое событие</h2>" +
               $"<p>Время: {evt.Timestamp}</p>" +
               $"<p>Метрика: {evt.Metric}</p>" +
               $"<p>Значение: {evt.Value}</p></body></html>";
    }
}

// 4. Интерфейс канала уведомлений
public interface INotificationChannel
{
    void Send(string message);
}

// 5. Реализации каналов
public class ConsoleNotificationChannel : INotificationChannel
{
    public void Send(string message)
    {
        Console.WriteLine(message);
    }
}

public class FileNotificationChannel : INotificationChannel
{
    private string _filePath;

    public FileNotificationChannel(string filePath)
    {
        _filePath = filePath;
    }

    public void Send(string message)
    {
        File.AppendAllText(_filePath, message + Environment.NewLine);
    }
}

// 6. Подписчик (наблюдатель)
public class Subscriber
{
    private readonly INotificationChannel _channel;
    private readonly IMessagingStrategy _strategy;

    public Subscriber(INotificationChannel channel, IMessagingStrategy strategy)
    {
        _channel = channel;
        _strategy = strategy;
    }

    public void Notify(Event evt)
    {
        string message = _strategy.FormatMessage(evt);
        _channel.Send(message);
    }
}

// 7. Базовый класс с шаблонным методом
public abstract class AlertHandler
{
    public void HandleEvent(Event evt)
    {
        string message = CreateMessage(evt);
        SendNotification(message);
    }

    protected abstract string CreateMessage(Event evt);
    protected abstract void SendNotification(string message);
}

// 8. Мониторинг и генерация событий
public class Monitor
{
    private List<Subscriber> _subscribers = new List<Subscriber>();
    private Random _random = new Random();

    public void Subscribe(Subscriber subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void CheckMetrics()
    {
        // Пример случайных данных
        int cpuUsage = _random.Next(0, 101);
        int memUsage = _random.Next(0, 101);
        int networkActivity = _random.Next(0, 2001);

        if (cpuUsage > 90)
        {
            var evt = new Event("CPU Load", cpuUsage);
            NotifySubscribers(evt);
        }
        if (memUsage > 90)
        {
            var evt = new Event("Memory Usage", memUsage);
            NotifySubscribers(evt);
        }
        if (networkActivity > 900)
        {
            var evt = new Event("Network Activity", networkActivity);
            NotifySubscribers(evt);
        }
    }

    private void NotifySubscribers(Event evt)
    {
        foreach (var sub in _subscribers)
        {
            sub.Notify(evt);
        }
    }

    public async Task RunAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            CheckMetrics();
            await Task.Delay(5000);
        }
    }
}

// 9. Этот пример запуска
public class Program
{
    public static async Task Main()
    {
        // Создаем стратегии
        IMessagingStrategy textStrategy = new TextMessagingStrategy();
        IMessagingStrategy jsonStrategy = new JsonMessagingStrategy();
        IMessagingStrategy htmlStrategy = new HtmlMessagingStrategy();

        // Создаем каналы
        var consoleChannel = new ConsoleNotificationChannel();
        var fileChannel = new FileNotificationChannel("alerts.log");

        // Создаем подписчиков
        var subscriber1 = new Subscriber(consoleChannel, textStrategy);
        var subscriber2 = new Subscriber(fileChannel, jsonStrategy);
        var subscriber3 = new Subscriber(consoleChannel, htmlStrategy);

        // Создаем монитор и подключаем подписчиков
        var monitor = new Monitor();
        monitor.Subscribe(subscriber1);
        monitor.Subscribe(subscriber2);
        monitor.Subscribe(subscriber3);

        // Запускаем мониторинг
        var cts = new System.Threading.CancellationTokenSource();

        Console.WriteLine("Мониторинг запущен. Нажмите Enter для остановки...");
        var runningTask = monitor.RunAsync(cts.Token);
        Console.ReadLine();
        cts.Cancel();
        await runningTask;
        Console.WriteLine("Мониторинг остановлен.");
    }
}