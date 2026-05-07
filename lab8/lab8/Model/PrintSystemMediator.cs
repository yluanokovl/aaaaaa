using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Model
{

    public class PrintSystemMediator : IMediator
    {
        private Dictionary<string, Action<Document>> handlers;

        private readonly Printer _printer;
        private readonly PrintQueue _queue;
        private readonly Logger _logger;
        private readonly Dispatcher _dispatcher;

        public PrintSystemMediator(Printer printer, PrintQueue printQueue, Logger logger, Dispatcher dispatcher)
        {
            _printer = printer;
            _queue = printQueue;
            _logger = logger;
            _dispatcher = dispatcher;
            _printer.SetMediator(this);
            _queue.SetMediator(this);
            _logger.SetMediator(this);
            _dispatcher.SetMediator(this);
            handlers= new Dictionary<string, Action<Document>>();
            handlers.Add("QueueDocument", doc => //вызывается от документа после перехода состояний
            {
                doc.SetMediator(this);
                _queue.Add(doc);
            });

            handlers.Add("Enqueued", doc => //вызывается очередью после добавления
            {
                _logger.WriteMessage($"{doc.Name} added to the queue"); 
            });

            handlers.Add("PrintDocument", doc => //вызывается документом после перехода состояний
            {
                doc.SetMediator(this);
                printer.StartPrint(doc);
            });

            handlers.Add("PrintStarted", doc => //сообщается принтером
            {
                _logger.WriteMessage($"{doc.Name} is being printed");
            });

            handlers.Add("PrintFailed", doc => //сообщается принтером
            {
                doc.SetMediator(this);
                doc.FailPrinting();
                _logger.WriteMessage($"{doc.Name} failed to print. Please reset.");
            });

            handlers.Add("PrintSucceeded", doc => //сообщается принтером
            {
                doc.SetMediator(this);
                doc.CompletePrinting();
                _logger.WriteMessage($"{doc.Name} was printed successfuly.");
            });

            handlers.Add("ProcessQueue", doc => //вызывается диспетчером
            {
                Document temp;
                _logger.WriteMessage($"Proccessing a queue of {_queue.Count} documents.");
                while (_queue.Count > 0)
                {
                    temp = _queue.Get();
                    temp?.Print();
                }
            });
        }

        public void Notify(Colleague sender, string ev, Document? document = null)
        {
            if (handlers is not null && handlers.TryGetValue(ev, out var action))
            {
                action(document); 
            }
            else
            {
                _logger.WriteMessage($"[Logger] Unknown Mediator request '{ev}'");
            }
        }
    }
}
