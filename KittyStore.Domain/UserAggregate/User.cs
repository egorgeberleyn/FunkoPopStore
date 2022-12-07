using KittyStore.Domain.Common.Models;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId>
{
    public string FirstName { get; }

    public string LastName { get; }

    public string Email { get; }

    public string Password { get; }

    public DateTime CreatedDateTime { get; }
    
    public DateTime UpdatedDateTime { get; }

    private User(UserId id, string firstName, string lastName, string email, string password, 
        DateTime createdDateTime, DateTime updatedDateTime) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }
    
    public static User Create(string firstName, string lastName, string email, string password) =>
        new(UserId.CreateUnique(), firstName, lastName, email, password, 
            DateTime.UtcNow, DateTime.UtcNow);
}