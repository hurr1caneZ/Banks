namespace Banks.Entitites.Builders;

public class ClientBuilder
{
    private string _firstName;
    private string _lastName;
    private string _address;
    private PassportNumber _passportNumber;

    public Client Build()
    {
        if (_firstName == default || _lastName == default)
            throw new ArgumentException("Client parametrs is not valid");
        return new Client(_firstName, _lastName, _address, _passportNumber);
    }

    public ClientBuilder SetFirstName(string firstName)
    {
        if (string.IsNullOrEmpty(firstName)) throw new ArgumentException("firstname is null or empty");
        _firstName = firstName;
        return this;
    }

    public ClientBuilder SetLastName(string lastName)
    {
        if (string.IsNullOrEmpty(lastName)) throw new ArgumentException("lastName is null or empty");
        _lastName = lastName;
        return this;
    }

    public ClientBuilder SetAddress(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new ArgumentException("address is null or empty");
        _address = address;
        return this;
    }

    public ClientBuilder SetPassportNumber(string passportNumber)
    {
        _passportNumber = new PassportNumber(passportNumber);
        return this;
    }

    // ss
}