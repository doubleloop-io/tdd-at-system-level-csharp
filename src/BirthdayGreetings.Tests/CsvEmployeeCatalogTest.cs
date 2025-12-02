using static TestSupport;

namespace BirthdayGreetings.Tests;

public class CsvEmployeeCatalogTest
{
    /**
     * DONE - Empty csv
     * Many lines
     * Missing file
     */

    [Fact]
    public async Task Empty()
    {
        var employeeFile = "csv-employee-catalog-test.csv";
        PrepareEmployeeFile(employeeFile, []);
        var catalog = new CsvEmployeeCatalog(employeeFile);

        var employees = await catalog.LoadEmployeesAsync();
        
        Assert.Empty(employees);
    }
}