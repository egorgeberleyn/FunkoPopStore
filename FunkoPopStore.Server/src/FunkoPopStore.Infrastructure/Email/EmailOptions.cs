namespace FunkoPopStore.Infrastructure.Email;

public class EmailOptions
{
    public const string SectionName = "EmailOptions";
    public const string SmtpLogFilePath = "smtp.log";

    public string Email { get; set; } = null!;
    public string EmailName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string SmtpAddress { get; set; } = null!;
    public int SmtpPort { get; set; }
}