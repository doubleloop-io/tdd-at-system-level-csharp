using System.Net.Mail;

namespace BirthdayGreetings;

public interface IPostalOffice
{
    Task SendGreetingsMessage(string name, string email, CancellationToken cancellationToken);
}

public class SmtpPostalOffice(string smtpHost, int smtpPort) : IPostalOffice
{
    public async Task SendGreetingsMessage(string name, string email, CancellationToken cancellationToken)
    {
        using var msg = new MailMessage(
            "greetings@acme.com",
            email,
            "Happy birthday!",
            $"Happy birthday, dear {name}!");

        try
        {
            using var smtp = new SmtpClient(smtpHost, smtpPort);
            await smtp.SendMailAsync(msg, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Smtp server unreachable at {smtpHost}:{smtpPort}", ex);
        }
    }
}