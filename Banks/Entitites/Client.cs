namespace Banks.Entitites;

public class Client
{
    public Client(string firstName, string lastName, string address, PassportNumber passportNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PassportNumber = passportNumber;
    }

    public string FirstName { get; internal set; }
    public string LastName { get; internal set; }
    public string Address { get; internal set; }
    public PassportNumber PassportNumber { get; internal set; }
    public Guid Id { get; internal set; }

    // ss
}