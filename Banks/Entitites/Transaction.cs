using Banks.Entitites.Accounts.Interfaces;

namespace Banks.Entitites;

public class Transaction
{
    public Transaction(Guid id, IBankAccount sender, IBankAccount receiver, decimal transactionSum)
    {
        TransactionId = id;
        if (TransactionSum < 0 || receiver is null || sender is null)
            throw new ArgumentException("Incorrect arguments");
        TransactionSum = transactionSum;
        SenderAccount = sender;
        ReceiverAccount = receiver;
    }

    public bool IsCancelled => false;
    public Guid TransactionId { get; internal set; }
    public IBankAccount SenderAccount { get; internal set; }
    public IBankAccount ReceiverAccount { get; internal set; }
    public decimal TransactionSum { get; internal set; }
    public void Execute()
    {
        SenderAccount.WithdrawFromAccount(TransactionSum);
        ReceiverAccount.AccountReplenishment(TransactionSum);
    }

    public void Undo()
    {
        if (IsCancelled)
            throw new ArgumentException("Transaction is already cancelled");
        SenderAccount.AccountReplenishment(TransactionSum);
        ReceiverAccount.WithdrawFromAccount(TransactionSum);
    }

    // ss
}