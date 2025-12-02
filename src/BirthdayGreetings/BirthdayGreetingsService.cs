namespace BirthdayGreetings;

public class BirthdayGreetingsService
{
    CsvEmployeeCatalog csvEmployeeCatalog;
    SmtpPostalOffice smtpPostalOffice;

    public BirthdayGreetingsService(CsvEmployeeCatalog csvEmployeeCatalog, SmtpPostalOffice smtpPostalOffice)
    {
        this.csvEmployeeCatalog = csvEmployeeCatalog;
        this.smtpPostalOffice = smtpPostalOffice;
    }

    public async Task RunAsync(DateOnly today, CancellationToken cancellationToken)
    {
        var employees = await csvEmployeeCatalog.LoadAsync();

        foreach (var employee in employees) {    
            if (employee.IsBirthday(today))
            {
                await smtpPostalOffice.SendMail(employee.FirstName, employee.Email, cancellationToken);
            }
        }
    }
}