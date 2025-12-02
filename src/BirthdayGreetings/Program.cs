using BirthdayGreetings;

Console.WriteLine("Welcome to the Birthday Greetings kata");

var employeeFile = "employees-hr.csv";
DemoData.SeedCsv(employeeFile, ManyEmployees());
var service = new BirthdayGreetingsService(new CsvEmployeeCatalog(employeeFile), new SmtpPostalOffice("localhost", 1025));
await service.RunAsync(DateOnly.Parse("2025-09-11"), CancellationToken.None);

Employee[] ManyEmployees() => [
    new("Al", "Capone", BirthDate.From("1951-06-02"), "al.capone@acme.com"),
    new("Pablo", "Escobar", BirthDate.From("1975-09-11"), "pablo.escobar@acme.com"),
    new("John", "Wick", BirthDate.From("1987-06-02"), "john.wick@acme.com"),
];