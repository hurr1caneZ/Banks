using System.Dynamic;
using Banks.Entitites;
using Banks.Entitites.Builders;
using Xunit;
using Assert = Xunit.Assert;

namespace Banks.Test;

public class BanksTest
{
    private CentralBank _centralBank = CentralBank.GetCentralBank();
    private BankBuilder _bankBuilder1 = new BankBuilder();

    public BanksTest()
    {
        _bankBuilder1.SetBankName("Sber")
            .SetBigDeposit(150000)
            .SetCreditComission(2)
            .SetCreditLimit(200000)
            .SetSmallDeposit(50000)
            .SetLowPercent(1)
            .SetYearPercent(2)
            .SetMidPercent(2)
            .SetHighPercent(3)
            .SetDepositTermDays(365);
    }

    [Fact]
    public void CheckCentralBankTransferMoneyAndCancelTransactionAndSkipTime()
    {
        Time time = new Time();
        Bank? bank1 = _centralBank.AddBank(_bankBuilder1);
        Client client1 = _centralBank.AddClientToBank(
            new ClientBuilder()
                .SetAddress("safasdfagsdf")
                .SetFirstName("abobus")
                .SetLastName("avtobus")
                .SetPassportNumber("1234567890")
                .Build(), bank1.Id);
        var acc1 = _centralBank.AddBankAccount(bank1.Id, "credit");
        var acc2 = _centralBank.AddBankAccount(bank1.Id, "debit");
        acc2.AccountReplenishment(20000);
        var transaction = _centralBank.TransferMoney(acc1.Id, acc2.Id, 10000);
        Assert.Equal(10000.0, (double)acc1.GetBalance());
        transaction.Undo();
        Assert.Equal(0.0, (double)acc1.GetBalance());
        acc1.WithdrawFromAccount(33);
        _centralBank.SkipTime(365);
        Assert.Equal(-763, acc1.GetBalance());
    }
    
    //ss
}