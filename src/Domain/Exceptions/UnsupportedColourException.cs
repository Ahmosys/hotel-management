namespace HotelManagement.Domain.Exceptions;

public class UnsupportedColourException : Exception
{
    // Here is a Domain Exception that is thrown when a colour is not supported.
    public UnsupportedColourException(string code)
        : base($"Colour \"{code}\" is unsupported.")
    {
    }
}
