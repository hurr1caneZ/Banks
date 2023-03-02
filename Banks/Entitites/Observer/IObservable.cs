namespace Banks.Entitites;

public interface IObservable
{
    void AddObserver(Observer.IObserver observer);
    void NotifyObservers(int days);

    // ss
}