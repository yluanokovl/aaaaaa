using System;
using System.Collections.Generic;
using System.Text;

namespace lab7.Model
{
    public interface IFormatStrategy
    {
        string Format(string message, DateTime timestamp);
    }
}
