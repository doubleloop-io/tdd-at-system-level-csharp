namespace BirthdayGreetings.Tests;

public class PostalOfficeSpy : IPostalOffice
{
    public List<GreetingsMessage> ReceivedMessages { get; } = new();

    public Task SendGreetingsMessage(GreetingsMessage message, CancellationToken cancellationToken)
    {
        ReceivedMessages.Add(message);
        return Task.CompletedTask;
    }
}