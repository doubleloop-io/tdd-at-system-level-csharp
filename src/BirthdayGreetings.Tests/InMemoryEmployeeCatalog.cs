namespace BirthdayGreetings.Tests;

public class InMemoryEmployeeCatalog(List<Employee> employees) : IEmployeeCatalog
{
    List<Employee> Employees { get; } = employees;
    
    public async Task<List<Employee>> LoadAsync()
    {
        return Employees.ToList();
    }
}