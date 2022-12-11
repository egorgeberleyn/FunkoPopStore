using KittyStore.Domain.CatAggregate.Enums;
using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.Common.Models;

namespace KittyStore.Domain.CatAggregate;

public sealed class Cat : AggregateRoot<CatId>
{
    public string Name { get; private set; }
    
    public int Age { get; private set;}
    
    public CatGender Gender { get; private set; }
    
    public string Color { get; private set;}
    
    public string Breed { get; private set;}
    
    public decimal Price { get; private set;}
    
    private Cat(CatId id, string name, int age, string color, string breed, decimal price, CatGender gender) : base(id)
    {
        Name = name;
        Age = age;
        Color = color;
        Breed = breed;
        Price = price;
        Gender = gender;
    }

    public static Cat Create(string name, int age, string color, string breed, decimal price, CatGender gender) =>
        new(CatId.CreateUnique(), name, age, color, breed, price, gender);

    public static Cat Update(Cat cat, string name, int age, string color, string breed, decimal price, CatGender gender)
    {
        cat.Name = name;
        cat.Age = age;
        cat.Breed = breed;
        cat.Color = color;
        cat.Price = price;
        cat.Gender = gender;

        return cat;
    }
}