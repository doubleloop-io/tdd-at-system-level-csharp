namespace BirthdayGreetings;

public static class DemoData
{
    public static void SeedCsv(string employeeFile, Employee[] employees)
    {
        var lines = employees
            .Select(e => $"{e.LastName}, {e.FirstName}, {e.BornOn}, {e.Email}")
            .ToArray();
        
        File.WriteAllLines(employeeFile, [
            "last_name, first_name, date_of_birth, email",
            ..lines
        ]);
    }
    
}