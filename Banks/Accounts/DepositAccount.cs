using Banks.Entitites;
using Banks.Entitites.Accounts.Interfaces;

namespace Banks.Accounts;

public class DepositAccount : IBankAccount
{
    private Guid _id;

    public DepositAccount(Guid id, decimal deposit, Bank bank)
    {
        Bank = bank;
        _id = id;
        if (deposit <= 50000)
            CurrentPercent = Bank.LowPercent;
        if (deposit >= 50000 && deposit <= 100000)
            CurrentPercent = Bank.MidPercent;
        if (deposit >= 100000)
            CurrentPercent = Bank.HighPercent;
        else
            throw new ArgumentException("Deposit is not founded :c");
        OpenDepositDate = Time.CurrentTime;
    }

    public Bank Bank { get; internal set; }
    public decimal CurrentPercent { get; internal set; }
    public DateTime OpenDepositDate { get; internal set; }
    public decimal Balance { get; internal set; }
    public bool IsTermEnded => (Time.CurrentTime - OpenDepositDate).Days > Bank.DepositTermDays;
    public Time Time { get; internal set; }

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
            sum = CurrentPercent / 365 * Balance / 100;
        }

        Balance += sum;
    }

    public decimal GetBalance()
    {
        return Balance;
    }

    public decimal WithdrawFromAccount(decimal amount)
    {
        if (IsTermEnded && Balance - amount >= 0)
            return Balance - amount;
        throw new ArgumentException("Term is not end");
    }

    public decimal AccountReplenishment(decimal amount)
    {
        return Balance + amount;
    }

    public override string ToString()
    {
        return $"depositAccount Guid: {_id}";
    }
}