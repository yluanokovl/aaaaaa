namespace lab8.Model
{
    public class Dispatcher : Colleague
    {
        public void CommandProcessQueue()
        {
            // Диспетчер дает команду посреднику начать обработку очереди
            Mediator.Notify(this, "ProcessQueue");
        }
    }
}
