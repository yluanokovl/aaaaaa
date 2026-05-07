using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Model
{
    public class Document : Colleague
    {
        IDocumentState State;
        public string Name { get; set; } = $"untitled_{DateTime.Now}";

        public Document(String name, IDocumentState state)
        {
            State = state;
            Name = name;
        }
        public void SetState(IDocumentState state) => State = state;

        // Делегирование поведения текущему состоянию
        public void Print() => State.Print(this);
        public void AddToQueue() => State.AddToQueue(this);
        public void CompletePrinting() => State.CompletePrinting(this);
        public void FailPrinting() => State.FailPrinting(this);
        public void Reset() => State.Reset(this);

        public void MediatorNotify(string ev)
        {
            Mediator.Notify(this, ev, this);
        }
    }
}
