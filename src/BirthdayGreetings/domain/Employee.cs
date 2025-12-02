namespace BirthdayGreetings;

public record Employee(string FirstName, string LastName, BirthDate BornOn, string Email)
{
    public bool IsBirthday(DateOnly today)
    {
        return BornOn.IsBirthday(today);
    }
}