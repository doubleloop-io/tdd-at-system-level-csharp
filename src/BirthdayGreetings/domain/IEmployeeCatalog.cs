namespace BirthdayGreetings;

public interface IEmployeeCatalog
{
    Task<List<Employee>> LoadAsync();
}