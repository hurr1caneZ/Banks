using Banks.Entitites;
using Banks.Entitites.Accounts.Interfaces;
using Banks.Exceptions;

namespace Banks.Accounts;

public class CreditAccount : IBankAccount
{
    private Guid _id;

    public CreditAccount(Guid id, Bank bank)
    {
        Bank = bank;
        CreditLimit = bank.CreditLimit;
        _id = id;
        CreditComission = bank.CreditComission;
    }

    public decimal Balance { get; internal set; }
    public decimal CreditComission { get; internal set; }
    public decimal CreditLimit { get; internal set; }
    public Bank Bank { get; internal set; }

    Guid IBankAccount.Id
    {
        get => _id;
        set => _id = value;
    }

    public decimal WithdrawFromAccount(decimal amount)
    {
        if (Balance - amount >= -Bank.CreditLimit)
            return Balance -= amount;
        throw new NegativeBalanceException();
    }

    public decimal AccountReplenishment(decimal amount)
    {
        return Balance += amount;
    }

    public decimal GetBalance()
    {
        return Balance;
    }

    public void Update(int skipedDays)
    {
        for (int i = 0; i < skipedDays; i++)
        {
            if (Balance < 0)
                Balance -= CreditComission;
        }
    }

    public override string ToString()
    {
        return $"creditAccount Guid: {_id}";
    }
}