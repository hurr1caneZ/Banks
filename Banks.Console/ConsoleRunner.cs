    using Banks.Entitites;
    using Banks.Entitites.Accounts;
    using BankBuilder = Banks.Entitites.Builders.BankBuilder;

    namespace Banks.Console;

public class ConsoleRunner
{
    private readonly CentralBank _centralBank;

    public ConsoleRunner()
    {
        _centralBank = CentralBank.GetCentralBank();
    }

    public void StartMenu()
    {
        System.Console.WriteLine("Welcome to the Bank System");
        System.Console.WriteLine("1. Bank Menu");
        System.Console.WriteLine("2. Customer Menu");
        System.Console.WriteLine("3. Time Menu");
        System.Console.WriteLine("4. Transaction Menu");
        int choice = int.Parse(System.Console.ReadLine() ?? string.Empty);
        switch (choice)
        {
            case 1:
                BankMenu();
                break;
            case 2:
                ClientMenu();
                break;
            case 3:
                TimeMenu();
                break;
            case 4:
                TransactionMenu();
                break;
            default:
                System.Console.WriteLine("Invalid choice. Please try again.");
                StartMenu();
                break;
        }
    }

    public void BankMenu()
    {
        System.Console.WriteLine("1. Create a bank");

        int choice = int.Parse(System.Console.ReadLine() ?? string.Empty);

        switch (choice)
        {
            case 1:
                NewBank();
                break;
            default:
                System.Console.WriteLine("Invalid choice. Please try again.");
                BankMenu();
                break;
        }
    }

    public void ClientMenu()
    {
        System.Console.WriteLine("1. Add client to bank");
        System.Console.WriteLine("2. Create an account");
        System.Console.WriteLine("3. Check balance");

        int choice = Convert.ToInt32(System.Console.ReadLine());
        switch (choice)
        {
            case 1:
                AddClientToBank();
                break;
            case 2:
                AddAccountToClient();
                break;
            case 3:
                GetAccountBalance();
                break;
            default:
                System.Console.WriteLine("Invalid choice. Please try again.");
                ClientMenu();
                break;
        }
    }

    private void TimeMenu()
    {
        System.Console.WriteLine("Enter the time which you want to skip (in days): ");
        int days = Convert.ToInt32(System.Console.ReadLine());
        _centralBank.SkipTime(days);
        System.Console.WriteLine($"Days skiped: {days}");
        GetAccountBalance();
    }

    private void TransactionMenu()
    {
        System.Console.WriteLine("1. Withdraw money from bankAccount");
        System.Console.WriteLine("2. Deposit money to bankAccount");
        System.Console.WriteLine("3. TransferMoney");

        int choice = Convert.ToInt32(System.Console.ReadLine());
        switch (choice)
        {
            case 1:
                WithdrawMoney();
                break;
            case 2:
                DepositMoney();
                break;
            case 3:
                TransferMoney();
                break;
            default:
                System.Console.WriteLine("Invalid choice. Please try again.");
                TransactionMenu();
                break;
        }
    }

    private void NewBank()
    {
        BankBuilder builder = new BankBuilder();
        System.Console.WriteLine("Enter a bank name");
        string bankName = System.Console.ReadLine() ?? string.Empty;
        builder.SetBankName(bankName);
        System.Console.WriteLine("Enter a lowest percent: ");
        builder.SetLowPercent(Convert.ToDecimal(System.Console.ReadLine()));
        System.Console.WriteLine("Enter a mid percent: ");
        builder.SetMidPercent(Convert.ToDecimal(System.Console.ReadLine()));
        System.Console.WriteLine("Enter a high percent: ");
        builder.SetHighPercent(Convert.ToDecimal(System.Console.ReadLine()));
        System.Console.WriteLine("Enter a year percent: ");
        builder.SetYearPercent(Convert.ToDecimal(System.Console.ReadLine()));
        System.Console.WriteLine("Enter a credit comission: ");
        builder.SetCreditComission(Convert.ToDecimal(System.Console.ReadLine()));
        System.Console.WriteLine("Enter a creadit limit: ");
        builder.SetCreditLimit(Convert.ToInt32(System.Console.ReadLine()));
        System.Console.WriteLine("Enter a small deposit: ");
        builder.SetSmallDeposit(Convert.ToInt32(System.Console.ReadLine()));
        System.Console.WriteLine("Enter a big deposit: ");
        builder.SetBigDeposit(Convert.ToInt32(System.Console.ReadLine()));
        builder.SetDepositTermDays(10000);
        Bank bank = _centralBank.AddBank(builder);
        System.Console.WriteLine($"Bank with name \"{bankName}\" created!\n");
        StartMenu();
    }

    private void AddClientToBank()
    {
        System.Console.Write("Enter a bank: ");
        string bankName = System.Console.ReadLine() ?? string.Empty;
        System.Console.Write("Enter a client first name: ");
        string clientFirstName = System.Console.ReadLine() ?? string.Empty;
        System.Console.Write("Enter a client second name: ");
        string clientSecondName = System.Console.ReadLine() ?? string.Empty;
        System.Console.Write("Enter an adress, to skip enter ENTER click once: ");
        string? address = System.Console.ReadLine();
        System.Console.Write("Enter a passport (10 numbers) to skip enter ENTER click once: ");
        PassportNumber? passport = null;
        string? passportNumber = System.Console.ReadLine();
        if (!string.IsNullOrEmpty(passportNumber))
            passport = new PassportNumber(passportNumber);
        Client client = new Client(clientFirstName, clientSecondName, address, passport);
        var bank = _centralBank.GetBank(bankName);
        _centralBank.AddClientToBank(client, bank.Id);
        System.Console.WriteLine($"Client \"{clientFirstName} {clientSecondName}\" with id \"{client.Id}\" in bank \"{bankName}\" was created!");
        ClientMenu();
    }

    private void AddAccountToClient()
    {
        System.Console.WriteLine("Enter a bank to create account: ");
        string bankName = System.Console.ReadLine() ?? string.Empty;
        Bank bank = _centralBank.GetBank(bankName);
        System.Console.WriteLine("Enter id client to get acc: ");
        Guid clientId = Guid.Parse(System.Console.ReadLine() ?? string.Empty);
        Client client = bank.GetClient(clientId);
        System.Console.WriteLine("Enter type of acc: ");
        System.Console.WriteLine("1. CreditAcc");
        System.Console.WriteLine("2. DebitAcc");
        System.Console.WriteLine("3. DepositAcc");
        int userChoose = Convert.ToInt32(System.Console.ReadLine());
        switch (userChoose)
        {
            case 1:
                var bankAccount1 = new CreditAccount(bank.Id, bank);
                System.Console.WriteLine("Enter the desired balance");
                var balance = Convert.ToDecimal(System.Console.ReadLine() ?? string.Empty);
                _centralBank.AddBankAccount(bank.Id, "credit", balance);
                break;
            case 2:
                var bankAccount2 = new DebitAccount(bank.Id, bank);
                _centralBank.AddBankAccount(bank.Id, "debit", 0);
                break;
            case 3:
                System.Console.WriteLine("Enter a deposit");
                var deposit = Convert.ToDecimal(System.Console.ReadLine());
                var bankAccount3 = new DepositAccount(bank.Id, deposit, bank);
                _centralBank.AddBankAccount(bank.Id, "deposit", deposit);
                System.Console.WriteLine("Enter days when depositacc is finished (in days)");
                System.Console.ReadLine();
                break;
            default:
                throw new ArgumentException("Try again");
        }

        System.Console.WriteLine($"Client {client.FirstName} was create an account in {bankName} bank\"");
        StartMenu();
    }

    private void GetAccountBalance()
    {
        System.Console.Write("Enter a bank name: ");
        string bankName = System.Console.ReadLine() ?? string.Empty;
        Bank bank = _centralBank.GetBank(bankName);

        System.Console.Write("Enter a id of acc: ");
        Guid id = Guid.Parse(System.Console.ReadLine() ?? string.Empty);
        var acc = _centralBank.GetBank(bank.BankName).GetAcc(id);
        System.Console.WriteLine($"Account with id \"{acc.Id}\" has {acc.GetBalance()} on balance");
        ClientMenu();
    }

    private void WithdrawMoney()
    {
        System.Console.Write("Enter a bank: ");
        string bankName = System.Console.ReadLine() ?? string.Empty;
        System.Console.Write("Enter an id of account: ");
        var accountId = Guid.Parse(System.Console.ReadLine() ?? string.Empty);
        System.Console.Write("Enter money to withdraw: ");
        decimal amountMoney = Convert.ToDecimal(System.Console.ReadLine());
        _centralBank.GetBank(bankName).GetAcc(accountId).WithdrawFromAccount(amountMoney);
        System.Console.WriteLine($"From account was withdraw {amountMoney}");
        TransactionMenu();
    }

    private void DepositMoney()
    {
        System.Console.Write("Enter a bank: ");
        string bankName = System.Console.ReadLine() ?? string.Empty;
        Bank bank = _centralBank.GetBank(bankName);
        System.Console.Write("Enter id of account: ");
        var accountNumber = Guid.Parse(System.Console.ReadLine() ?? string.Empty);
        System.Console.Write("Enter a money to deposit ");
        decimal amountMoney = Convert.ToDecimal(System.Console.ReadLine());
        bank.GetAcc(accountNumber).AccountReplenishment(amountMoney);
        System.Console.WriteLine($"Account was topped up with {amountMoney}");
        TransactionMenu();
    }

    private void TransferMoney()
    {
        System.Console.Write("Enter a Sender id of account to transfer money: ");
        var senderId = Guid.Parse(System.Console.ReadLine() ?? string.Empty);
        System.Console.Write("Enter the Getter id of account to claim money: ");
        var receiverId = Guid.Parse(System.Console.ReadLine() ?? string.Empty);
        System.Console.Write("Enter amount of money to transfer: ");
        decimal amountMoney = Convert.ToDecimal(System.Console.ReadLine());
        _centralBank.TransferMoney(receiverId, senderId, amountMoney);
        System.Console.WriteLine($"{amountMoney} was successfully transfered from \"{receiverId}\" to \"{senderId}\"");
        TransactionMenu();
    }
}