using System.Net.Mail;

namespace BirthdayGreetings;

public class BirthdayGreetingsService
{
    private string employeeFile;
    private string smtpHost;
    private int smtpPort;

    public BirthdayGreetingsService(string employeeFile, string smtpHost, int smtpPort)
    {
        this.employeeFile = employeeFile;
        this.smtpHost = smtpHost;
        this.smtpPort = smtpPort;
    }

    public async Task RunAsync(DateOnly today, CancellationToken cancellationToken)
    {
        if (!File.Exists(employeeFile))
            throw new Exception($"Employee file does not exists: {employeeFile}");

        var allLines = await File.ReadAllLinesAsync(employeeFile);
        var employeeLines = allLines.Skip(1).ToArray();
        
        foreach (var employeeLine in employeeLines)
        {
            var employeeParts = employeeLine
                .Split(",")
                .Select(x => x.Trim())
                .ToArray();

            var employee = new Employee(
                employeeParts[1], 
                employeeParts[0],
                BirthDate.From(employeeParts[2]),
                employeeParts[3]);

            if (employee.IsBirthday(today))
            {
                await SendMail(employee.FirstName, employee.Email, cancellationToken);
            }
        }
    }

    private async Task SendMail(
        string name,
        string email, 
        CancellationToken cancellationToken)
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