namespace BirthdayGreetings;

public interface IPostalOffice
{
    Task SendGreetingsMessage(GreetingsMessage message, CancellationToken cancellationToken);
}