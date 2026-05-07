using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Model
{
    public class QueuedState : IDocumentState
    {
        public void Print(Document document)
        {
            document.SetState(new PrintingState());
            Console.WriteLine($"[FSM: Queued -> Printing] Документ {document.Name} поступил в печать.");
            document.MediatorNotify("PrintDocument");
        }

        public void AddToQueue(Document document) => Console.WriteLine($"[FSM: Queued] Документ {document.Name} уже в очереди.");

        public void CompletePrinting(Document document) => Console.WriteLine($"[FSM: Queued] Документ {document.Name} еще не печатается.");

        public void FailPrinting(Document document) => Console.WriteLine($"[FSM: Queued] Документ {document.Name} еще не печатается.");

        public void Reset(Document document) => Console.WriteLine($"[FSM: Queued] Документ {document.Name} не в состоянии ошибки.");
    }
}

