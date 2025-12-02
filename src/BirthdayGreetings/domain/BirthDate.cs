using System.Globalization;

namespace BirthdayGreetings;

public record BirthDate
{
    BirthDate(DateOnly Value) => 
        this.Value = Value;

    public static BirthDate From(string value) => 
        new(DateOnly.Parse(value));
    
    public bool IsBirthday(DateOnly date)
    {
        return Value.Month == date.Month && Value.Day == date.Day;
    }

    DateOnly Value { get; }

    public override string ToString() => 
        Value.ToString("yyyy-MM-dd");
}