using lab8.Model;

namespace lab8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Printer printer = new Printer();
            Logger logger = new Logger();
            Dispatcher dispatcher = new Dispatcher();
            PrintQueue queue = new PrintQueue();
            PrintSystemMediator med = new PrintSystemMediator(printer, queue, logger, dispatcher);

            IDocumentState s = new NewState();
            Document doc1 = new Document("aaa", s);
            Document doc2 = new Document("f", s);
            doc1.SetMediator(med);
            doc2.SetMediator(med);

            doc1.AddToQueue();
            doc2.AddToQueue();
            dispatcher.CommandProcessQueue();

            Console.WriteLine();
            printer.SimulateFailure = true;
            Document doc3 = new Document("fail", s);
            doc3.SetMediator(med);
            doc3.AddToQueue();
            dispatcher.CommandProcessQueue();

            Console.WriteLine();
            printer.SimulateFailure = false;
            doc3.Reset();
            doc3.AddToQueue();
            dispatcher.CommandProcessQueue();
        }
    }
}
