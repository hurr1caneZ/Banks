namespace Banks.Entitites;

public class PassportNumber
{
    public PassportNumber(string passport)
    {
        if (passport.Length is > 10 or < 10) throw new ArgumentException("Passport length is wrong");
    }

    // ss
}