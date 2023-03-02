namespace Banks.Exceptions;

public class NegativeBalanceException : Exception
{
    public NegativeBalanceException()
        : base("Negative balance alert!")
    { }

    // ss
}