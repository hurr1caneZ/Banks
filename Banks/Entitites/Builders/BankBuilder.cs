namespace Banks.Entitites.Builders;

public class BankBuilder
{
    private string _bankName;
    private decimal _lowPercent;
    private decimal _midPercent;
    private decimal _highPercent;
    private decimal _yearPercent;
    private decimal _creditComission;
    private decimal _creditLimit;
    private decimal _smallDeposit;
    private decimal _bigDeposit;
    private int _depositTermDays;

    public Bank Build()
    {
        if (_bankName == default || _lowPercent == default || _midPercent == default || _highPercent == default || _yearPercent == default ||
            _creditComission == default || _creditLimit == default || _smallDeposit == default || _bigDeposit == default || _depositTermDays == default)
            throw new ArgumentException("Bankname is not valid");
        return new Bank(_bankName, _lowPercent, _midPercent, _highPercent, _yearPercent, _creditComission, _creditLimit, _smallDeposit, _bigDeposit, _depositTermDays);
    }

    public BankBuilder SetBankName(string bankName)
    {
        if (string.IsNullOrEmpty(bankName)) throw new ArgumentException("BankName is null or empty");
        _bankName = bankName;
        return this;
    }

    public BankBuilder SetLowPercent(decimal lowPercent)
    {
        if (lowPercent < 0) throw new ArgumentException("Lowpercent is incorrect");
        _lowPercent = lowPercent;
        return this;
    }

    public BankBuilder SetMidPercent(decimal midPercent)
    {
        if (midPercent < 0) throw new ArgumentException("Midpercent is incorrect");
        _midPercent = midPercent;
        return this;
    }

    public BankBuilder SetHighPercent(decimal highPercent)
    {
        if (highPercent < 0) throw new ArgumentException("highPercent is incorrect");
        _highPercent = highPercent;
        return this;
    }

    public BankBuilder SetYearPercent(decimal yearPercent)
    {
        if (yearPercent < 0) throw new ArgumentException("yearPercent is incorrect");
        _yearPercent = yearPercent;
        return this;
    }

    public BankBuilder SetCreditComission(decimal creditComission)
    {
        if (creditComission < 0) throw new ArgumentException("creditComission is incorrect");
        _creditComission = creditComission;
        return this;
    }

    public BankBuilder SetCreditLimit(decimal creditLimit)
    {
        if (creditLimit < 0) throw new ArgumentException("CreditLimit is incorrect");
        _creditLimit = creditLimit;
        return this;
    }

    public BankBuilder SetSmallDeposit(decimal smallDeposit)
    {
        if (smallDeposit < 0) throw new ArgumentException("SmallDeposit is incorrect");
        _smallDeposit = smallDeposit;
        return this;
    }

    public BankBuilder SetBigDeposit(decimal bigDeposit)
    {
        if (bigDeposit < 0) throw new ArgumentException("BidDeposit is incorrect");
        _bigDeposit = bigDeposit;
        return this;
    }

    public BankBuilder SetDepositTermDays(int depositTermDays)
    {
        if (depositTermDays < 0 || depositTermDays == 0) throw new ArgumentException("DepositTermDays is incorrect");
        _depositTermDays = depositTermDays;
        return this;
    }

    // ss
}