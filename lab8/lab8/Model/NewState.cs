using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Model
{
    public class NewState : IDocumentState
    {
        public void Print(Document document)
        {
            document.AddToQueue();
            Console.WriteLine($"[FSM: New -> Queued] Документ {document.Name} еще не в очереди. Документ будет отправлен в очередь.");
        }

        public void AddToQueue(Document document)
        {
            document.MediatorNotify("QueueDocument");
            document.SetState(new QueuedState());
            Console.WriteLine($"[FSM: New->Queued] Документ {document.Name} в очереди на печать.");
        }

        public void CompletePrinting(Document document) => Console.WriteLine($"[FSM: New] Документ {document.Name} не в очереди или еще не напечатан.");

        public void FailPrinting(Document document) => Console.WriteLine($"[FSM: New] {document.Name} Документ не в очереди или еще не напечатан.");

        public void Reset(Document document) => Console.WriteLine($"[FSM: New] {document.Name} Документ не в состоянии ошибки.");
    }
}
