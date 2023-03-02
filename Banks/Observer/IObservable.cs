namespace Banks.Observer;

public interface IObservable
{
    void AddObserver(IObserver observer);
    void NotifyObservers(int days);

    // ss
}