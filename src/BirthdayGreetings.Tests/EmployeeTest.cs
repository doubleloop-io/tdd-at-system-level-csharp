namespace BirthdayGreetings.Tests;

public class EmployeeTest
{
    [Fact]
    public void BirthDay()
    {
        var john = new Employee(
            "John", 
            "Wick", 
            DateOnly.Parse("1984-09-27"), 
            "john.wick@acme.com");
        
        Assert.True(john.IsBirthday(DateOnly.Parse("2025-09-27")));
    }
    
    [Fact]
    public void SameMonthDifferentDay()
    {
        var john = new Employee(
            "John", 
            "Wick", 
            DateOnly.Parse("1984-09-27"), 
            "john.wick@acme.com");
        
        Assert.False(john.IsBirthday(DateOnly.Parse("2025-09-01")));
    }
    
    [Fact]
    public void DifferentMonthSameDay()
    {
        var john = new Employee(
            "John", 
            "Wick", 
            DateOnly.Parse("1984-02-27"), 
            "john.wick@acme.com");
        
        Assert.False(john.IsBirthday(DateOnly.Parse("2025-09-27")));
    }
}