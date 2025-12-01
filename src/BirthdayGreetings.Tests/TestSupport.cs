using netDumbster.smtp;

public static class TestSupport
{
    public static void PrepareEmployeeFile(string employeeFile, string[] content)
    {
        File.WriteAllLines(employeeFile, [
            "last_name, first_name, date_of_birth, email",
            ..content
        ]);
    }
    
    public static void AssertContainsGreetingMessage(SmtpMessage[] receivedEmails, string name, string email)
    {
        var received = receivedEmails
            .Single(x => x.ToAddresses.Any(y => y.Address == email));
        
        Assert.Equal("greetings@acme.com", received.FromAddress.Address);
        Assert.Equal(email, received.ToAddresses[0].Address);
        Assert.Equal("Happy birthday!", received.Subject);
        Assert.Equal($"Happy birthday, dear {name}!", received.MessageParts[0].BodyData);
    }
}