using ErrorOr;

namespace KittyStore.Application.Common.Interfaces.Email;

public interface IEmailService
{
    Task<ErrorOr<Success>> SendAsync(string email, string subject, string message);
}