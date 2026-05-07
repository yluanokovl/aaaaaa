using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Model
{
    public interface IDocumentState
    {
        void Print(Document document);
        void AddToQueue(Document document);
        void CompletePrinting(Document document);
        void FailPrinting(Document document);
        void Reset(Document document);
    }
}
