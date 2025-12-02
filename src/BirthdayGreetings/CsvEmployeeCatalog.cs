namespace BirthdayGreetings;

public interface IEmployeeCatalog
{
    Task<List<Employee>> LoadAsync();
}

public class CsvEmployeeCatalog(string employeeFile) : IEmployeeCatalog
{
    public async Task<List<Employee>> LoadAsync()
    {
        if (!File.Exists(employeeFile))
            throw new Exception($"Employee file does not exists: {employeeFile}");

        var allLines = await File.ReadAllLinesAsync(employeeFile);
        var employeeLines = allLines.Skip(1).ToArray();
        
        var employees = new List<Employee>();
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
            employees.Add(employee);
        }

        return employees;
    }
}