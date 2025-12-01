namespace BirthdayGreetings;

public record BirthDate(DateOnly Value)
{
    public bool IsBirthday(DateOnly date)
    {
        return Value.Month == date.Month && Value.Day == date.Day;
    }
}