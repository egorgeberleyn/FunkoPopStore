using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.CatAggregate;

public sealed class Cat : AggregateRoot<CatId>
{
    public string Name { get; }
    
    public int Age { get; }
    
    public string Color { get; }
    
    public string Breed { get; }
    
    public decimal Price { get; }


    public Cat(CatId id, string name, int age, string color, string breed, decimal price) : base(id)
    {
        Name = name;
        Age = age;
        Color = color;
        Breed = breed;
        Price = price;
    }

    public static Cat Create(string name, int age, string color, string breed, decimal price) =>
        new(CatId.CreateUnique(), name, age, color, breed, price);

}