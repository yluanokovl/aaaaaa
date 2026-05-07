using System;
using System.Collections.Generic;
using System.Text;

namespace lab7.Model
{
    public abstract class EventHandlerBase
    {
        protected IFormatStrategy _formatStrategy; //текущая стратегия
        protected EventHandlerBase(IFormatStrategy strategy)
        {
            _formatStrategy = strategy;
        }
        //Метод установки стратегии
        public void SetStrategy(IFormatStrategy strategy)
        {
            _formatStrategy = strategy;
        }

        public abstract string FormatMessage(string type, object data);

        public abstract void SendMessage(string message);

        public abstract void LogResult();


        public virtual void ProcessEvent(MetricEventArgs e)
        {
            var message = FormatMessage(e.EventType, e.Data); //форматируем по стратегии
            SendMessage(message); //отправляем уведомление
            LogResult(); //логируем результат (опционально)
        }
    }
}
