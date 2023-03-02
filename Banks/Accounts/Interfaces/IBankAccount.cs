using Banks.Entitites;

namespace Banks.Accounts.Interfaces;

public interface IBankAccount : IObserver
{
    public Guid Id { get; internal set; }
    decimal WithdrawFromAccount(decimal amount);
    decimal AccountReplenishment(decimal amount);
    decimal GetBalance();
}