using FunkoPopStore.Domain.Common.Primitives;

namespace FunkoPopStore.Domain.OrderAggregate.ValueObjects;

public class Address : ValueObject
{
    public string Country { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string HouseNumber { get; private set; }

    public Address(string country, string city, string street, string houseNumber)
    {
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
    }

    public static Address Create(string country, string city, string street, string houseNumber) =>
        new(country, city, street, houseNumber);

    public override string ToString()
        => $"{HouseNumber} {Street}, {City}, {Country}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Country;
        yield return City;
        yield return Street;
        yield return HouseNumber;
    }
}