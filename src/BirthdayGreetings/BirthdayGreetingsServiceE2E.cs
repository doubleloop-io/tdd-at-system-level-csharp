namespace BirthdayGreetings;

public class BirthdayGreetingsServiceE2E
{
    IEmployeeCatalog employeeCatalog;
    IPostalOffice postalOffice;

    public BirthdayGreetingsServiceE2E(IEmployeeCatalog employeeCatalog, IPostalOffice postalOffice)
    {
        this.employeeCatalog = employeeCatalog;
        this.postalOffice = postalOffice;
    }

    public async Task RunAsync(DateOnly today, CancellationToken cancellationToken)
    {
        var employees = await employeeCatalog.LoadAsync();

        foreach (var employee in employees) {    
            if (employee.IsBirthday(today))
            {
                await postalOffice.SendGreetingsMessage(employee.FirstName, employee.Email, cancellationToken);
            }
        }
    }
}