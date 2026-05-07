using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Model
{
    public class PrintQueue : Colleague
    {
        private Queue<Document> _queue = new Queue<Document>();

        public void Add(Document doc) 
        {
            _queue.Enqueue(doc);
            if (Mediator is not null)
                Mediator.Notify(this, "Enqueued", doc);
        }
        
        public Document? Get() => _queue.Count > 0 ? _queue.Dequeue() : null;
        public int Count => _queue.Count;
    }
}
