namespace BirthdayGreetings.Tests;

public class BirthDateTest
{
    [Fact]
    public void BirthDay()
    {
        var birthDate = BirthDate.From("1984-09-27");
        
        Assert.True(birthDate.IsBirthday(DateOnly.Parse("2025-09-27")));
    }
    
    [Fact]
    public void SameMonthDifferentDay()
    {
        var birthDate = BirthDate.From("1984-09-27");
        
        Assert.False(birthDate.IsBirthday(DateOnly.Parse("2025-09-01")));
    }
    
    [Fact]
    public void DifferentMonthSameDay()
    {
        var birthDate = BirthDate.From("1984-02-27");
        
        Assert.False(birthDate.IsBirthday(DateOnly.Parse("2025-09-27")));
    }
}