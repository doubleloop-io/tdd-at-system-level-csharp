using MongoDB.Bson;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace BirthdayGreetings.Tests;

public class MongoEmployeeCatalogTest: IAsyncLifetime
{
    private readonly ITestOutputHelper output;

    public MongoEmployeeCatalogTest(ITestOutputHelper output)
    {
        this.output = output;
    }
    
    private MongoDbContainer? container;
    private string dbName = "mongo-employee-repository-test";

    public async ValueTask InitializeAsync()
    {
        container = new MongoDbBuilder().Build();
        await container.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await container?.StopAsync();
    }
    
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
    
        var results = await collection
            .Find(Builders<EmployeeDoc>.Filter.Empty)
            .ToListAsync();
        
        var es = results.Select(EmployeeDoc.To).ToList();
        output.WriteLine($"Here some results {es.Count}");
    }
}

public record EmployeeDoc(ObjectId _id, 
    string FirstName, string LastName, 
    DateOnly DateOfBirth, string Email)
{
    public static EmployeeDoc From(Employee e)
    {
        return new EmployeeDoc(new ObjectId(), e.FirstName, e.LastName, e.BornOn.Value, e.Email);
    }

    public static Employee To(EmployeeDoc e)
    {
        
        return new Employee(e.FirstName, e.LastName, new BirthDate(e.DateOfBirth), e.Email);
    }
}