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
    public async Task OneMessage()
    {
        var smtpPostalOffice = new SmtpPostalOffice(smtpHost, smtpPort);

        await smtpPostalOffice.SendMail(
            "Al", 
            "al.capone@acme.com", 
            TestContext.Current.CancellationToken);
        
        Assert.Equal(1, smtpServer.ReceivedEmailCount);
        AssertContainsGreetingMessage(
            smtpServer.ReceivedEmail,
            "Al", "al.capone@acme.com"
            );
    }
}