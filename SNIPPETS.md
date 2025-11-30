### Many Employees
```csharp
new Employee("Al", "Capone", BirthDate.From("1951-06-02"), "al.capone@acme.com"),
new Employee("Pablo", "Escobar", BirthDate.From("1975-09-11"), "pablo.escobar@acme.com"),
new Employee("John", "Wick", BirthDate.From("1987-06-02"), "john.wick@acme.com"),
```

### Run Mailpit with Docker
```bash
docker run --name=mailpit --rm -p 8025:8025 -p 1025:1025 axllent/mailpit
```

### Run Mongo with Docker
```bash
docker run --name tdd-mongo --rm -p 27017:27017 mongo:8
```

### EmployeeDoc
```csharp
public record EmployeeDoc(ObjectId _id, string FirstName, string LastName, DateTime DateOfBirth, string Email)
{
    public static EmployeeDoc From(Employee e)
    {
        var newBornOn = DateTime.SpecifyKind(e.BornOn.BornOn, DateTimeKind.Utc).ToUniversalTime();
        return new EmployeeDoc(new ObjectId(), e.FirstName, e.LastName, newBornOn, e.Email);
    }

    public static Employee To(EmployeeDoc e)
    {
        var newBornOn = DateTime.SpecifyKind(e.DateOfBirth, DateTimeKind.Utc).ToUniversalTime();
        return new Employee(e.FirstName, e.LastName, new BirthDate(newBornOn), e.Email);
    }
}
```

### xUnit AsyncLifecycle
```csharp
public class MongoEmployeeRepositoryTest(ITestOutputHelper output) : IAsyncLifetime
{
    private MongoDbContainer? container;
    private string dbName = "mongo-employee-repository-test";

    public async Task InitializeAsync()
    {
        container = new MongoDbBuilder().Build();
        await container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await container?.StopAsync();
    }
}
```

### MongoDb ItWorks()
```csharp
[Fact]
public async Task ItWorks()
{
    var client = new MongoClient(container?.GetConnectionString());
    var db = client.GetDatabase(dbName);
    var collection = db.GetCollection<EmployeeDoc>("employees");
    
    await collection.InsertOneAsync(
        EmployeeDoc.From(new Employee(
            "Massimo", 
            "Iacolare", 
            BirthDate.From("2025-06-02"), 
            "massimo.iacolare@acme.com"))
        );
    
    var results = await collection.Find(Builders<EmployeeDoc>.Filter.Empty).ToListAsync();
    var es = results.Select(EmployeeDoc.To).ToList();
    output.WriteLine($"Here some results {es.Count}");
}
```

