using Banks.Entitites;
using Banks.Entitites.Accounts.Interfaces;
using Banks.Exceptions;

namespace Banks.Accounts;

public class DebitAccount : IBankAccount
{
    private Guid _id;

    public DebitAccount(Guid id, Bank bank)
    {
        Bank = bank;
        _id = id;
        if (Balance < 0)
            throw new NegativeBalanceException();
        YearPercent = Bank.YearPercent;
    }

    public bool AbleToWithdraw => true;
    public decimal Balance { get; internal set; }
    public Bank Bank { get; internal set; }
    public decimal YearPercent { get; internal set; }

    Guid IBankAccount.Id
    {
        get => _id;
        set => _id = value;
    }

    public void Update(int skipedDays)
    {
        decimal sum = 0;
        for (int i = 0; i < skipedDays; i++)
        {
            sum = YearPercent / 365 * Balance / 100;
        }

        Balance += sum;
    }

    public decimal GetBalance()
    {
        return Balance;
    }

    public decimal WithdrawFromAccount(decimal amount)
    {
        if (Balance - amount >= 0)
            return Balance -= amount;
        throw new NegativeBalanceException();
    }

    public decimal AccountReplenishment(decimal amount)
    {
        return Balance += amount;
    }

    public override string ToString()
    {
        return $"debitAccount Guid: {_id}";
    }
}