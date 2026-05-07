namespace lab7.Model
{
    public class TextFormatStrategy : IFormatStrategy
    {
        public string Format(string message, DateTime timestamp) =>
        $"[{timestamp:yyyy-MM-dd HH:mm:ss}] {message}";
    }
}
