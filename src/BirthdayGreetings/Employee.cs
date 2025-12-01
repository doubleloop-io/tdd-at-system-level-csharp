namespace BirthdayGreetings;

public record Employee(string FirstName, string LastName, DateOnly BirthDate2, string Email)
{
    public bool IsBirthday(DateOnly today)
    {
        return new BirthDate(BirthDate2).IsBirthday(today);
    }
}