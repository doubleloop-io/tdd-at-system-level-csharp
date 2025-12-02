namespace BirthdayGreetings;

public class BirthdayGreetingsService
{
    CsvEmployeeCatalog csvEmployeeCatalog;
    IPostalOffice postalOffice;

    public BirthdayGreetingsService(CsvEmployeeCatalog csvEmployeeCatalog, IPostalOffice postalOffice)
    {
        this.csvEmployeeCatalog = csvEmployeeCatalog;
        this.postalOffice = postalOffice;
    }

    public async Task RunAsync(DateOnly today, CancellationToken cancellationToken)
    {
        var employees = await csvEmployeeCatalog.LoadAsync();

        foreach (var employee in employees) {    
            if (employee.IsBirthday(today))
            {
                await postalOffice.SendGreetingsMessage(employee.FirstName, employee.Email, cancellationToken);
            }
        }
    }
}