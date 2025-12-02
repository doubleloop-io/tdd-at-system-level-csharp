namespace BirthdayGreetings;

public class BirthdayGreetingsService(IEmployeeCatalog employeeCatalog, IPostalOffice postalOffice)
{
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