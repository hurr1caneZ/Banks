namespace Banks.Entitites.Accounts.Interfaces;

public interface IBankAccount : Observer.IObserver
{
    public Guid Id { get; internal set; }
    decimal WithdrawFromAccount(decimal amount);
    decimal AccountReplenishment(decimal amount);
    decimal GetBalance();

    // ss
}