using KittyStore.Domain.Common.Primitives;
using KittyStore.Domain.UserAggregate.Enums;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Domain.UserAggregate
{
    public sealed class User : AggregateRoot
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set;}

        public string Email { get; private set;}

        public byte[] PasswordHash { get; private set; }
    
        public byte[] PasswordSalt { get; private set; }
    
        public Balance? Balance { get; private set; }
    
        public Role Role { get; private set;}

        public DateTime CreatedDateTime { get; private set; }
    
        public DateTime UpdatedDateTime { get; private set;}

        private User(Guid id, string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt, 
            Role role, DateTime createdDateTime, DateTime updatedDateTime, Balance? balance = null) : base(id)
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
    
        public static User Create(string firstName, string lastName, string email, 
            byte[] passwordHash, byte[] passwordSalt, Role role, Balance? balance = null) =>
            new(Guid.NewGuid(), firstName, lastName, email, passwordHash, passwordSalt, role,
                DateTime.UtcNow, DateTime.UtcNow, balance);

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
        
        #pragma warning disable CS8618
            private User() { }
        #pragma warning disable CS8618
    }
}