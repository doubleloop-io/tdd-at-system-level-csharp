namespace BirthdayGreetings;

public class BirthdayGreetingsService
{
    IEmployeeCatalog employeeCatalog;
    IPostalOffice postalOffice;

    public BirthdayGreetingsService(IEmployeeCatalog employeeCatalog, IPostalOffice postalOffice)
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
                var msg = new GreetingsMessage(employee.FirstName, employee.Email);
                await postalOffice.SendGreetingsMessage(msg, cancellationToken);
            }
        }
    }
}