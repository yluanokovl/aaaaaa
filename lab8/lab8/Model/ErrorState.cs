using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab8.Model
{
    public class ErrorState : IDocumentState
    {
        public void Print(Document document) => Console.WriteLine($"[FSM: Error] Печать {document.Name} невозможна из - за ошибки.Сначала сбросьте документ(Reset).");

        public void AddToQueue(Document document) => Console.WriteLine($"[FSM: Error] Нельзя добавить {document.Name} в очередь из-за ошибки. Сначала сбросьте документ.");

        public void CompletePrinting(Document document) => Console.WriteLine($"[FSM: Error] Ошибка {document.Name} не устранена.");

        public void FailPrinting(Document document) => Console.WriteLine($"[FSM: Error] Документ {document.Name} уже в состоянии ошибки.");

        public void Reset(Document document)
        {
            document.SetState(new NewState());
            Console.WriteLine($"[FSM: Error -> New] Документ {document.Name} сброшен и готов к повторной печати.");
        }
    }
}
