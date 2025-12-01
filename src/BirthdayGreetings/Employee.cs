namespace BirthdayGreetings;

public record Employee(string FirstName, string LastName, DateOnly BirthDate, string Email)
{
    public bool IsBirthday(DateOnly today)
    {
        return BirthDate.Month == today.Month && BirthDate.Day == today.Day;
    }
}