using KittyStore.Domain.Common.Models;
using KittyStore.Domain.CustomerAggregate.ValueObjects;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Domain.CustomerAggregate;

public sealed class Customer : AggregateRoot<CustomerId>
{
    public string FirstName { get; }

    public string LastName { get; }
    
    public decimal Balance { get; }
    
    public UserId UserId { get; }
    
    public DateTime CreatedDateTime { get; }
    
    public DateTime UpdatedDateTime { get; }


    private Customer(CustomerId id, string firstName, string lastName, decimal balance, 
        UserId userId, DateTime createdDateTime, DateTime updatedDateTime) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Balance = balance;
        UserId = userId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Customer Create(CustomerId id, string firstName, string lastName,
       decimal balance, UserId userId) =>
            new(CustomerId.CreateUnique(), firstName, lastName, balance, userId, 
            DateTime.UtcNow, DateTime.UtcNow);
}