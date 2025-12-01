using System.Net.Mail;

namespace BirthdayGreetings;

public class BirthdayGreetingsService
{
    public async Task Run(DateOnly today, CancellationToken cancellationToken)
    {
        var allLines = await File.ReadAllLinesAsync("employee-e2e.csv");
        // for each employee
        // var employeeLines = allLines.Skip(1).ToArray();
        var employeeLine = allLines[1];
        var employeeParts = employeeLine
            .Split(",")
            .Select(x => x.Trim())
            .ToArray();
        var firstName = employeeParts[1];
        var birthDate = DateOnly.Parse(employeeParts[2]);
        var email = employeeParts[3];

        if (birthDate.Month == today.Month && birthDate.Day == today.Day)
        {
            using var msg = new MailMessage(
                "greetings@acme.com",
                email,
                "Happy birthday!",
                $"Happy birthday, dear {firstName}!");
        
            using var smtp = new SmtpClient("localhost", 1025);
            await smtp.SendMailAsync(msg, cancellationToken);
        }
    }
}