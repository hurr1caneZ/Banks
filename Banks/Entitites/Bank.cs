using System.Runtime.InteropServices.ComTypes;
using Banks.Entitites.Accounts;
using Banks.Entitites.Accounts.Interfaces;
using Guid = System.Guid;

namespace Banks.Entitites;

public class Bank
{
    private List<Observer.IObserver> _observers;
    private Dictionary<Guid, IBankAccount> _bankAccounts;
    private Dictionary<Guid, Client> _clients;
    public Bank(string bankName, decimal lowPercent, decimal midPercent, decimal highPercent, decimal yearPercent, decimal creditComission, decimal creditLimit, decimal smallDeposit, decimal bigDeposit, int depositTermDays)
    {
        _observers = new List<Observer.IObserver>();
        _bankAccounts = new Dictionary<Guid, IBankAccount>();
        _clients = new Dictionary<Guid, Client>();
        BankName = bankName;
        LowPercent = lowPercent;
        MidPercent = midPercent;
        HighPercent = highPercent;
        YearPercent = yearPercent;
        CreditComission = creditComission;
        CreditLimit = creditLimit;
        SmallDeposit = smallDeposit;
        BigDeposit = bigDeposit;
        DepositTermDays = depositTermDays;
    }

    public decimal LowPercent { get; internal set; }
    public decimal MidPercent { get; internal set; }
    public decimal HighPercent { get; internal set; }
    public decimal YearPercent { get; internal set; }
    public decimal CreditComission { get; internal set; }
    public decimal CreditLimit { get; internal set; }
    public decimal SmallDeposit { get; internal set; }
    public decimal BigDeposit { get; internal set; }
    public int DepositTermDays { get; internal set; }
    public Guid Id { get; internal set; }
    public string BankName { get; internal set; }
    public void AddClient(Client client)
    {
        _clients.Add(client.Id, client);
    }

    public Client GetClient(Guid id)
    {
        if (_clients.TryGetValue(id, out Client client))
        {
            return client;
        }

        throw new Exception("Client is not found");
    }

    public IBankAccount AddBankAccount(Guid id, string accountType, decimal initialBalance = 0)
    {
        IBankAccount bankAccount;
        switch (accountType)
        {
            case "debit":
                bankAccount = new DebitAccount(id, this);
                break;
            case "credit":
                bankAccount = new CreditAccount(id, this);
                break;
            case "deposit":
                if (initialBalance <= 0) throw new ArgumentException("Initialbalance for deposit account must be more than zero");
                bankAccount = new DepositAccount(id, initialBalance, this);
                break;
            default:
                throw new ArgumentException($"Account with type {accountType} doesnt exist");
        }

        _bankAccounts.Add(id, bankAccount);
        return bankAccount;
    }

    public IBankAccount GetAcc(Guid id)
    {
        foreach (var ids in _bankAccounts.Keys)
        {
            if (ids == id)
                return _bankAccounts[ids];
        }

        throw new Exception("account is not found");
    }
}