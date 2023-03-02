using System.Data;
using System.Runtime.CompilerServices;
using Banks.Entitites.Accounts.Interfaces;
using Banks.Entitites.Builders;

namespace Banks.Entitites;

public class CentralBank : IObservable
{
    private static CentralBank _centralBankValue;
    private List<Observer.IObserver> _observers;
    private Dictionary<Guid, Bank> _banks;
    private Dictionary<Guid, IBankAccount> _accounts;
    private Time _time;
    private CentralBank()
    {
        _time = new Time();
        _observers = new List<Observer.IObserver>();
        _banks = new Dictionary<Guid, Bank>();
        _accounts = new Dictionary<Guid, IBankAccount>();
    }

    public static CentralBank GetCentralBank()
    {
        if (_centralBankValue == null)
            _centralBankValue = new CentralBank();
        return _centralBankValue;
    }

    public Dictionary<Guid, Bank> GetBanks() => _banks;

    public Time GetTime() => _time;

    public Bank AddBank(BankBuilder bankBuilder)
    {
        var bankId = Guid.NewGuid();
        if (bankBuilder is null) throw new ArgumentException("Incorrect bankbuilder");
        Bank bank = bankBuilder.Build();
        _banks.Add(bankId, bank);
        bank.Id = bankId;
        return bank;
    }

    public Bank GetBank(string name)
    {
        foreach (var bank in _banks.Values)
        {
            if (bank.BankName == name)
                return bank;
        }

        throw new Exception("bank is not found");
    }

    public Client AddClientToBank(Client client, Guid bankId)
    {
        var id = Guid.NewGuid();
        if (client == null)
        {
            throw new ArgumentNullException(nameof(client), "Client cannot be null.");
        }

        if (!_banks.ContainsKey(bankId))
            throw new ArgumentException("bank doesn't exist");
        client.Id = id;
        _banks[bankId].AddClient(client);
        return client;
    }

    public IBankAccount AddBankAccount(Guid bankId, string accountType, decimal initialBalance = 0)
    {
        var accountId = Guid.NewGuid();
        if (!_banks.ContainsKey(bankId)) throw new ArgumentException("Bank doesn't exist");
        IBankAccount newAcc = _banks[bankId].AddBankAccount(accountId, accountType, initialBalance);
        AddObserver(newAcc);
        _accounts.Add(accountId, newAcc);
        return newAcc;
    }

    public void SkipTime(int days)
    {
        _time.SkipTimeForDays(days);
        NotifyObservers(days);
    }

    public void AddObserver(Observer.IObserver observer)
    {
        _observers.Add(observer);
    }

    public void NotifyObservers(int days)
    {
        foreach (Observer.IObserver observer in _observers)
        {
            observer.Update(days);
        }
    }

    public Transaction TransferMoney(Guid receiverId, Guid senderId, decimal sum)
    {
        IBankAccount receiver = _accounts[receiverId];
        IBankAccount sender = _accounts[senderId];
        var transaction = new Transaction(Guid.NewGuid(), sender, receiver, sum);
        transaction.Execute();
        return transaction;
    }
}