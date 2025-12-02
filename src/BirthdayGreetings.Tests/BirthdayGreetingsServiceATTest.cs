using netDumbster.smtp;

namespace BirthdayGreetings.Tests;

public class BirthdayGreetingsServiceATTest: IDisposable
{
    private string employeeFile = "employee-at.csv";
    private string smtpHost = "localhost";
    private int smtpPort = 1036;
    private readonly SimpleSmtpServer smtpServer;

    public BirthdayGreetingsServiceATTest()
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
        TestSupport.PrepareEmployeeFile(employeeFile, [
            "Capone, Al, 1951-10-08, al.capone@acme.com",
            "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
            "Wick, John, 1987-02-17, john.wick@acme.com"
        ]);
        // var employeeCatalog = new InMemoryEmployeeCatalog();
        var service = new BirthdayGreetingsService(new CsvEmployeeCatalog(employeeFile), new SmtpPostalOffice(smtpHost, smtpPort));
        
        await service.RunAsync(DateOnly.Parse("2025-12-01"), TestContext.Current.CancellationToken);

        Assert.Equal(0, smtpServer.ReceivedEmailCount);
    }
    
    [Fact]
    public async Task ManyBirthdays()
    {
        TestSupport.PrepareEmployeeFile(employeeFile, [
            "Capone, Al, 1951-09-11, al.capone@acme.com",
            "Wick, John, 1987-02-17, john.wick@acme.com",
            "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
        ]);
        var service = new BirthdayGreetingsService(new CsvEmployeeCatalog(employeeFile), new SmtpPostalOffice(smtpHost, smtpPort));
        
        await service.RunAsync(DateOnly.Parse("2025-09-11"), TestContext.Current.CancellationToken);

        Assert.Equal(2, smtpServer.ReceivedEmailCount);
        TestSupport.AssertContainsGreetingMessage(smtpServer.ReceivedEmail, "Al", "al.capone@acme.com");
        TestSupport.AssertContainsGreetingMessage(smtpServer.ReceivedEmail, "Pablo", "pablo.escobar@acme.com");
    }
}