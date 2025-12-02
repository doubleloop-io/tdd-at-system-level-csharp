namespace BirthdayGreetings.Tests;

public class BirthdayGreetingsServiceATTest
{
    [Fact]
    public async Task NoBirthday()
    {
        var employeeCatalog = new InMemoryEmployeeCatalog([
            new Employee("Al", "Capone", BirthDate.From("1951-10-08"), "al.capone@acme.com"),
            new Employee("Pablo", "Escobar", BirthDate.From("1975-09-11"), "pablo.escobar@acme.com"),
            new Employee("John", "Wick", BirthDate.From("1987-02-17"), "john.wick@acme.com"),
        ]);
        var postalOffice = new PostalOfficeSpy();
        var service = new BirthdayGreetingsService(employeeCatalog, postalOffice);
        
        await service.RunAsync(DateOnly.Parse("2025-12-01"), TestContext.Current.CancellationToken);

        Assert.Empty(postalOffice.ReceivedMessages);
    }
    
    [Fact]
    public async Task ManyBirthdays()
    {
        var employeeCatalog = new InMemoryEmployeeCatalog([
            new Employee("Al", "Capone", BirthDate.From("1951-09-11"), "al.capone@acme.com"),
            new Employee("John", "Wick", BirthDate.From("1987-02-17"), "john.wick@acme.com"),
            new Employee("Pablo", "Escobar", BirthDate.From("1975-09-11"), "pablo.escobar@acme.com"),
        ]);
        var postalOffice = new PostalOfficeSpy();
        var service = new BirthdayGreetingsService(employeeCatalog, postalOffice);
        
        await service.RunAsync(DateOnly.Parse("2025-09-11"), TestContext.Current.CancellationToken);

        Assert.Equal(2, postalOffice.ReceivedMessages.Count);
        Assert.Contains(new GreetingsMessage("Al", "al.capone@acme.com"), postalOffice.ReceivedMessages);
        Assert.Contains(new GreetingsMessage("Pablo", "pablo.escobar@acme.com"), postalOffice.ReceivedMessages);
    }
}