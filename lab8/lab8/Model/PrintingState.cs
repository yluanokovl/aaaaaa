using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Model
{
    public class PrintingState : IDocumentState
    {
        public void Print(Document document) => Console.WriteLine($"[FSM: Printing] Документ {document.Name} уже в печати.");

        public void AddToQueue(Document document) => Console.WriteLine($"[FSM: Printing] Документ {document.Name} уже в печати, нельзя добавить в очередь.");


        public void CompletePrinting(Document document)
        {
            document.SetState(new DoneState());
            Console.WriteLine($"[FSM: Printing -> Done] Печать {document.Name} успешно завершена."); 
        }

        public void FailPrinting(Document document)
        {
            document.SetState(new ErrorState());
            Console.WriteLine($"[FSM: Printing -> Error] Произошла ошибка при печати {document.Name}.");
        }

        public void Reset(Document document) => Console.WriteLine($"[FSM: Printing] Документ {document.Name} не в состоянии ошибки.");
    }
}
