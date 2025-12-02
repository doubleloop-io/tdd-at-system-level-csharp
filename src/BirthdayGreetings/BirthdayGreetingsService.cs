namespace BirthdayGreetings;

public class BirthdayGreetingsService
{
    CsvEmployeeCatalog csvEmployeeCatalog;
    SmtpPostalOffice smtpPostalOffice;

    public BirthdayGreetingsService(string employeeFile, SmtpPostalOffice smtpPostalOffice)
    {
        this.csvEmployeeCatalog = new CsvEmployeeCatalog(employeeFile);
        this.smtpPostalOffice = smtpPostalOffice;
    }

    public async Task RunAsync(DateOnly today, CancellationToken cancellationToken)
    {
        var employees = await csvEmployeeCatalog.LoadEmployeesAsync();

        foreach (var employee in employees) {    
            if (employee.IsBirthday(today))
            {
                await smtpPostalOffice.SendMail(employee.FirstName, employee.Email, cancellationToken);
            }
        }
    }
}