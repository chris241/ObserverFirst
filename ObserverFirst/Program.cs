public interface IObserver
{
    void Update(ISubject subject);
}
public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}
public class Subject : ISubject
{
    public int State { get; set; } = -0;
    private List<IObserver> _observers = new List<IObserver>();
    public void Attach(IObserver observer)
    {
        Console.WriteLine("Subject: Attached an observer");
        this._observers.Add(observer);  
    }

    public void Detach(IObserver observer)
    {
        
        this._observers.Remove(observer);
        Console.WriteLine("Subject: Detached an observer");
    }

    public void Notify()
    {
        Console.WriteLine("Subject: Notifying observers ...");
        foreach(var  observer in _observers)
        {
            observer.Update(this);  
        }
    }
    public void SomeBusinessLogic()
    {
        Console.WriteLine("\nSubject: I'm doing something important.");
        this.State = new Random().Next(1, 10);  
        Thread.Sleep(15);

        Console.WriteLine("Subject: My state has just changed to :" +this.State);
        this.Notify();
    }
    class ConcreteOberverA : IObserver
    {
        public void Update(ISubject subject)
        {
          if(((Subject)subject).State < 3)
            {
                Console.WriteLine("ConcreteOberverA : Reached to the event ");
            }
        }
    }
    class ConcreteOberverB : IObserver
    {
        public void Update(ISubject subject)
        {
            if (((Subject)subject).State == 0 || ((Subject)subject).State >= 2 )
            {
                Console.WriteLine("ConcreteOberverB : Reached to the event ");
            }
        }
    }
    class Program
    {
        static void Main(string[] args) 
        {
            var subject = new Subject();
            var observerA = new ConcreteOberverA(); 
            subject.Attach(observerA);

            var observerB = new ConcreteOberverB();
            subject.Attach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            subject.Detach(observerB);
            subject.SomeBusinessLogic();    
        }
    }
}
