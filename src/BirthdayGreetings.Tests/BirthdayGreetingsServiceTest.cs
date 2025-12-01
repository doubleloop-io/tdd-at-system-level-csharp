using System.Net.Mail;
using netDumbster.smtp;
using static TestSupport;

namespace BirthdayGreetings.Tests;

public class BirthdayGreetingsServiceTest: IDisposable
{
    private readonly SimpleSmtpServer smtpServer;

    public BirthdayGreetingsServiceTest()
    {
        smtpServer = SimpleSmtpServer.Start(1025);
    }

    public void Dispose()
    {
        smtpServer.Stop();
        smtpServer.Dispose();
    }

    [Fact]
    public async Task TestSendMail()
    {
        using var email = new MailMessage(
            "sender@acme.com",
            "recipient@acme.com",
            "Test subject",
            "Test body");
        
        using var smtp = new SmtpClient("localhost", 1025);
        await smtp.SendMailAsync(email, TestContext.Current.CancellationToken);
        
        Assert.Equal(1, smtpServer.ReceivedEmailCount);
        var received = smtpServer.ReceivedEmail[0];
        Assert.Equal("sender@acme.com", received.FromAddress.Address);
        Assert.Equal("recipient@acme.com", received.ToAddresses[0].Address);
        Assert.Equal("Test subject", received.Subject);
        Assert.Equal("Test body", received.MessageParts[0].BodyData);
    }

    [Fact]
    public void TestReadEmployeeFile()
    {
        TestSupport.PrepareEmployeeFile("employee-e2e.csv", [
            "Capone, Al, 1951-10-08, al.capone@acme.com",
            "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
            "Wick, John, 1987-09-11, john.wick@acme.com"
        ]);

        var allLines = File.ReadAllLines("employee-e2e.csv");
        
        Assert.Equal(4, allLines.Length);
    }

    [Fact]
    public async Task NoBirthday()
    {
        PrepareEmployeeFile("employee-e2e.csv", [
            "Capone, Al, 1951-10-08, al.capone@acme.com",
            "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
            "Wick, John, 1987-02-17, john.wick@acme.com"
        ]);
        var service = new BirthdayGreetingsService();
        
        await service.RunAsync(DateOnly.Parse("2025-12-01"), TestContext.Current.CancellationToken);

        Assert.Equal(0, smtpServer.ReceivedEmailCount);
    }
    
    [Fact]
    public async Task OneBirthday()
    {
        PrepareEmployeeFile("employee-e2e.csv", [
            "Wick, John, 1987-02-17, john.wick@acme.com",
            "Capone, Al, 1951-10-08, al.capone@acme.com",
            "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
        ]);
        var service = new BirthdayGreetingsService();
        
        await service.RunAsync(DateOnly.Parse("2025-02-17"), TestContext.Current.CancellationToken);

        Assert.Equal(1, smtpServer.ReceivedEmailCount);
        var received = smtpServer.ReceivedEmail[0];
        Assert.Equal("greetings@acme.com", received.FromAddress.Address);
        Assert.Equal("john.wick@acme.com", received.ToAddresses[0].Address);
        Assert.Equal("Happy birthday!", received.Subject);
        Assert.Equal("Happy birthday, dear John!", received.MessageParts[0].BodyData);
    }
}