using System.Net.Mail;
using netDumbster.smtp;

namespace BirthdayGreetings.Tests;

public class BirthdayGreetingsServiceTest
{
    [Fact]
    public void ItWorks()
    {
        Assert.Equal("a", "a");
    }

    [Fact(Skip = "Not there yet. too much stuff todo")]
    public void NoBirthday()
    {
        // prepare employee file
            // write file with some employees
            // prepare file in advance
        
        // BirthdayGreetingsService.Run(2025-12-01)
            // Read employee file
            // for each employee
            // if month and day matches
                // create mail
                // send mail
                
        // sent mail to smtp server is 0
    }

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
}