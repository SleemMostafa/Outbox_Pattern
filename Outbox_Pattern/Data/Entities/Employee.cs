namespace Outbox_Pattern.Data.Entities;

public sealed class Employee
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required decimal Salary { get; init; }
    public required DateTimeOffset Created { get; init; }

    public static  Employee CreateInstance(string name,string email,decimal salary)
    {
        return new Employee
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Email = email,
            Salary = salary,
            Created = DateTimeOffset.UtcNow,
        };
    }
}