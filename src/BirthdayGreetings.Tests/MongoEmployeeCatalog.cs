using MongoDB.Bson;
using MongoDB.Driver;

namespace BirthdayGreetings.Tests;

public class MongoEmployeeCatalog : IEmployeeCatalog
{
    private readonly IMongoCollection<EmployeeDoc> collection;

    public MongoEmployeeCatalog(string? connectionString, string dbName)
    {
        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(dbName);
        this.collection = db.GetCollection<EmployeeDoc>("employees");
    }

    public async Task<List<Employee>> LoadAsync()
    {
        var results = await collection
            .Find(Builders<EmployeeDoc>.Filter.Empty)
            .ToListAsync();

        return results.Select(EmployeeDoc.To).ToList();
    }
}

public record EmployeeDoc(
    ObjectId _id,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Email)
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