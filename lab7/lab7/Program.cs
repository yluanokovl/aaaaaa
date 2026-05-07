using lab7.Model;

namespace lab7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EventMonitor jajah = new EventMonitor();//наблюд
            jajah.OnMetricExceeded += e => Console.WriteLine("ну че там твин получилось проц разогнать?");//подп на соб
            jajah.CheckMetric();//проверка с событием
            JsonFormatStrategy json = new JsonFormatStrategy();
            TextFormatStrategy text = new TextFormatStrategy();
            ConsoleHandler console = new ConsoleHandler(json);
            FileHandler file = new FileHandler(text);
            file.ProcessEvent(new MetricEventArgs("rat", new MetricData("network", 1000, 100, DateTime.Now)));
            console.ProcessEvent(new MetricEventArgs("matgorit", new MetricData("temp", 250, 100, DateTime.Now)));
        }
    }
}
