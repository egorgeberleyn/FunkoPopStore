using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.OrderAggregate.ValueObjects;

public class Address : ValueObject
{
    public string Country { get; }
    public string City { get;}
    public string Street { get; }
    public string HouseNumber { get; }

    private Address(string country, string city, string street, string houseNumber)
    {
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
    }

    public static Address Create(string country, string city, string street, string houseNumber) => 
        new(country, city,  street, houseNumber);
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Country;
        yield return City;
        yield return Street;
        yield return HouseNumber;
    }
}