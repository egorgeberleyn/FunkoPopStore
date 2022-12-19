using KittyStore.Domain.Common.Models;
using KittyStore.Domain.UserAggregate.Enums;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Domain.UserAggregate
{
    public sealed class User : AggregateRoot<UserId>
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set;}

        public string Email { get; private set;}

        public byte[] PasswordHash { get; private set;}
    
        public byte[] PasswordSalt { get; private set;}
    
        public Balance Balance { get; private set; }
    
        public Role Role { get; private set; }

        public DateTime CreatedDateTime { get; private set;}
    
        public DateTime UpdatedDateTime { get; private set;}

        public User(UserId id, string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt, 
            Balance balance, Role role, DateTime createdDateTime, DateTime updatedDateTime) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
            Balance = balance;
            Role = role;
        }
    
        public static User Create(string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt, 
            Balance balance, Role role) =>
            new(UserId.CreateUnique(), firstName, lastName, email, passwordHash, passwordSalt, balance, role,
                DateTime.UtcNow, DateTime.UtcNow);

        public User Update(string firstName, string lastName, string email,
            Balance balance)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Balance = balance;
            UpdatedDateTime = DateTime.UtcNow;

            return this;
        }
    }
}