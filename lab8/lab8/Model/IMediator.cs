using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Model
{
    public interface IMediator
    {
        void Notify(Colleague sender, string ev, Document? document = null);
    }
}
