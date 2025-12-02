using netDumbster.smtp;
using static TestSupport;

namespace BirthdayGreetings.Tests;

public class SmtpPostalOfficeTest : IDisposable
{
    private string smtpHost = "localhost";
    private int smtpPort = 1027;
    private SimpleSmtpServer smtpServer;

    /** TODO:
     * - DONE One Message
     * - Many Messages
     * - Smtp unreachable
     */

    public SmtpPostalOfficeTest()
    {
        smtpServer = SimpleSmtpServer.Start(smtpPort);
    }

    public void Dispose()
    {
        smtpServer.Stop();
        smtpServer.Dispose();
    }
    
    [Fact]
    public async Task ManyMessages()
    {
        var smtpPostalOffice = new SmtpPostalOffice(smtpHost, smtpPort);

        await smtpPostalOffice.SendMail(
            "Al", 
            "al.capone@acme.com", 
            TestContext.Current.CancellationToken);
        await smtpPostalOffice.SendMail(
            "John", 
            "john.wick@acme.com", 
            TestContext.Current.CancellationToken);
        
        Assert.Equal(2, smtpServer.ReceivedEmailCount);
        AssertContainsGreetingMessage(
            smtpServer.ReceivedEmail,
            "Al", "al.capone@acme.com"
        );
        AssertContainsGreetingMessage(
            smtpServer.ReceivedEmail,
            "John", "john.wick@acme.com"
        );
    }
}