using static TestSupport;

namespace BirthdayGreetings.Tests;

public class CsvEmployeeCatalogTest
{
    private string employeeFile = "csv-employee-catalog-test.csv";

    [Fact]
    public async Task Empty()
    {
        PrepareEmployeeFile(employeeFile, []);
        var catalog = new CsvEmployeeCatalog(employeeFile);

        var employees = await catalog.LoadAsync();
        
        Assert.Empty(employees);
    }
    
    [Fact]
    public async Task Many()
    {
        PrepareEmployeeFile(employeeFile, [
            "Capone, Al, 1951-10-08, al.capone@acme.com",
            "Wick, John, 1987-02-17, john.wick@acme.com",
            "Escobar, Pablo, 1975-09-11, pablo.escobar@acme.com",
        ]);
        var catalog = new CsvEmployeeCatalog(employeeFile);

        var employees = await catalog.LoadAsync();
        
        Assert.Contains(new Employee("Al", "Capone", BirthDate.From("1951-10-08"), "al.capone@acme.com"), employees);
        Assert.Contains(new Employee("John", "Wick", BirthDate.From("1987-02-17"), "john.wick@acme.com"), employees);
        Assert.Contains(new Employee("Pablo", "Escobar", BirthDate.From("1975-09-11"), "pablo.escobar@acme.com"), employees);
    }
    
    [Fact]
    public async Task EmployeeFileMissing()
    {
        var notExistentFile = "this-does-not-exists.csv";
        var catalog = new CsvEmployeeCatalog(notExistentFile);
        
        var ex = await Record.ExceptionAsync(()  => 
            catalog.LoadAsync()
        );
        
        Assert.Contains("Employee file does not exists", ex.Message);
        Assert.Contains(notExistentFile, ex.Message);
    }
}