using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Model
{
    public class DoneState : IDocumentState
    {
        public void Print(Document document) => Console.WriteLine($"[FSM: Done] Работа с документом {document.Name} завершена, невозможно взаимодействовать.");

        public void AddToQueue(Document document) => Console.WriteLine($"[FSM: Done] Работа с документом {document.Name} завершена, невозможно взаимодействовать.");


        public void CompletePrinting(Document document) => Console.WriteLine($"[FSM: Done] Работа с документом {document.Name} завершена, невозможно взаимодействовать.");

        public void FailPrinting(Document document) => Console.WriteLine($"[FSM: Done] Работа с документом {document.Name} завершена, невозможно взаимодействовать.");

        public void Reset(Document document) => Console.WriteLine($"[FSM: Done] Работа с документом {document.Name} завершена, невозможно взаимодействовать.");
    }
}
