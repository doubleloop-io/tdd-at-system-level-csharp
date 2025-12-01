using System.Net.Mail;
using netDumbster.smtp;

namespace BirthdayGreetings.Tests;

public class BirthdayGreetingsServiceTest
{
    [Fact]
    public async Task TestSendMail()
    {
        using var server = SimpleSmtpServer.Start(1025);
        
        using var email = new MailMessage(
            "sender@acme.com",
            "recipient@acme.com",
            "Test subject",
            "Test body");
        
        using var smtp = new SmtpClient("localhost", 1025);
        await smtp.SendMailAsync(email, TestContext.Current.CancellationToken);
        
        Assert.Equal(1, server.ReceivedEmailCount);
        var received = server.ReceivedEmail[0];
        Assert.Equal("sender@acme.com", received.FromAddress.Address);
        Assert.Equal("recipient@acme.com", received.ToAddresses[0].Address);
        Assert.Equal("Test subject", received.Subject);
        Assert.Equal("Test body", received.MessageParts[0].BodyData);
    }

    [Fact]
    public void TestReadEmployeeFile()
    {
        File.WriteAllLines("employee-e2e.csv", [
            "last_name, first_name, date_of_birth, email",
            "Capone, Al, 1951-10-08, al.capone@acme.com",
            "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
            "Wick, John, 1987-09-11, john.wick@acme.com"
        ]);

        var allLines = File.ReadAllLines("employee-e2e.csv");
        
        Assert.Equal(4, allLines.Length);
    }

    [Fact]
    public void NoBirthday()
    {
        using var server = SimpleSmtpServer.Start(1025);
        File.WriteAllLines("employee-e2e.csv", [
            "last_name, first_name, date_of_birth, email",
            "Capone, Al, 1951-10-08, al.capone@acme.com",
            "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
            "Wick, John, 1987-09-11, john.wick@acme.com"
        ]);
        var service = new BirthdayGreetingsService();
        
        service.Run(DateOnly.Parse("2025-12-01"));

        Assert.Equal(0, server.ReceivedEmailCount);
    }
}

public class BirthdayGreetingsService
{
    public void Run(DateOnly today)
    {
        // Read employee file
        // for each employee
        // if month and day matches
        // create mail
        // send mail
    }
}