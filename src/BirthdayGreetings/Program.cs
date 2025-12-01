// See https://aka.ms/new-console-template for more information

using BirthdayGreetings;

Console.WriteLine("Welcome to the Birthday Greetings kata");

var employeeFile = "/Users/iacoware/projects/training/qubica-2025/onsite/tdd-at-system-level/data/employees.csv";
var service = new BirthdayGreetingsService(employeeFile, "localhost", 1025);
await service.RunAsync(DateOnly.Parse("2025-09-11"), CancellationToken.None);
