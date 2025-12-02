namespace BirthdayGreetings;

public class BirthdayGreetingsService
{
    private string employeeFile;
    SmtpPostalOffice smtpPostalOffice;

    public BirthdayGreetingsService(string employeeFile, SmtpPostalOffice smtpPostalOffice)
    {
        this.employeeFile = employeeFile;
        this.smtpPostalOffice = smtpPostalOffice;
    }

    public async Task RunAsync(DateOnly today, CancellationToken cancellationToken)
    {
        if (!File.Exists(employeeFile))
            throw new Exception($"Employee file does not exists: {employeeFile}");

        var allLines = await File.ReadAllLinesAsync(employeeFile);
        var employeeLines = allLines.Skip(1).ToArray();
        
        foreach (var employeeLine in employeeLines)
        {
            var employeeParts = employeeLine
                .Split(",")
                .Select(x => x.Trim())
                .ToArray();

            var employee = new Employee(
                employeeParts[1], 
                employeeParts[0],
                BirthDate.From(employeeParts[2]),
                employeeParts[3]);

            if (employee.IsBirthday(today))
            {
                await smtpPostalOffice.SendMail(employee.FirstName, employee.Email, cancellationToken);
            }
        }
    }
}