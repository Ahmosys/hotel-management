namespace HotelManagement.Domain.Exceptions;

public class UnsupportedColourException(string code) : Exception($"Colour \"{code}\" is unsupported.")
{
}
