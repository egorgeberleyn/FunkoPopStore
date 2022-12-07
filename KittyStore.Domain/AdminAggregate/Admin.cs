using KittyStore.Domain.AdminAggregate.ValueObjects;
using KittyStore.Domain.Common.Models;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Domain.AdminAggregate;

public sealed class Admin : AggregateRoot<AdminId>
{
    public string FirstName { get; }

    public string LastName { get; }
    
    public UserId UserId { get; }
    
    public DateTime CreatedDateTime { get; }
    
    public DateTime UpdatedDateTime { get; }

    private Admin(AdminId id, string firstName, string lastName, UserId userId, 
        DateTime createdDateTime, DateTime updatedDateTime) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        UserId = userId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }
    
    public static Admin Create(AdminId id, string firstName, string lastName, UserId userId) =>
        new(AdminId.CreateUnique(), firstName, lastName, userId, 
            DateTime.UtcNow, DateTime.UtcNow);
}