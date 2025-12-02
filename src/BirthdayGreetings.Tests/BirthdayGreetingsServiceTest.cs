using netDumbster.smtp;
using static TestSupport;

namespace BirthdayGreetings.Tests;

public class BirthdayGreetingsServiceTest: IDisposable
{
    private string employeeFile = "employee-e2e.csv";
    private string smtpHost = "localhost";
    private int smtpPort = 1026;
    private readonly SimpleSmtpServer smtpServer;

    public BirthdayGreetingsServiceTest()
    {
        smtpServer = SimpleSmtpServer.Start(smtpPort);
    }

    public void Dispose()
    {
        smtpServer.Stop();
        smtpServer.Dispose();
    }

    [Fact]
    public async Task NoBirthday()
    {
        PrepareEmployeeFile(employeeFile, [
            "Capone, Al, 1951-10-08, al.capone@acme.com",
            "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
            "Wick, John, 1987-02-17, john.wick@acme.com"
        ]);
        var service = new BirthdayGreetingsService(new CsvEmployeeCatalog(employeeFile), new SmtpPostalOffice(smtpHost, smtpPort));
        
        await service.RunAsync(DateOnly.Parse("2025-12-01"), TestContext.Current.CancellationToken);

        Assert.Equal(0, smtpServer.ReceivedEmailCount);
    }
    
    [Fact]
    public async Task ManyBirthdays()
    {
        PrepareEmployeeFile(employeeFile, [
            "Capone, Al, 1951-09-11, al.capone@acme.com",
            "Wick, John, 1987-02-17, john.wick@acme.com",
            "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
        ]);
        var service = new BirthdayGreetingsService(new CsvEmployeeCatalog(employeeFile), new SmtpPostalOffice(smtpHost, smtpPort));
        
        await service.RunAsync(DateOnly.Parse("2025-09-11"), TestContext.Current.CancellationToken);

        Assert.Equal(2, smtpServer.ReceivedEmailCount);
        AssertContainsGreetingMessage(smtpServer.ReceivedEmail, "Al", "al.capone@acme.com");
        AssertContainsGreetingMessage(smtpServer.ReceivedEmail, "Pablo", "pablo.escobar@acme.com");
    }
}