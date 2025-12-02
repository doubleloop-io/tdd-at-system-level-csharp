using System.Net.Mail;

namespace BirthdayGreetings;

public class SmtpPostalOffice(string smtpHost, int smtpPort) : IPostalOffice
{
    public async Task SendGreetingsMessage(GreetingsMessage message, CancellationToken cancellationToken)
    {
        using var msg = new MailMessage(
            "greetings@acme.com",
            message.Email,
            "Happy birthday!",
            $"Happy birthday, dear {message.Name}!");

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