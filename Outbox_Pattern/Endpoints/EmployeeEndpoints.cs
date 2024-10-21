using System.Text.Json;
using Outbox_Pattern.Data;
using Outbox_Pattern.Data.Entities;

namespace Outbox_Pattern.Endpoints;

public static class EmployeeEndpoints
{
    public static void MapEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/create", async (EmployeeCreatedRequest request,Db db) =>
        {
            var employee = Employee.CreateInstance( request.Name, request.Email, request.Salary);
            
            var outboxMsg = OutboxMessage.CreateInstance($"event: {nameof(EmployeeCreatedRequest)}", JsonSerializer.Serialize(request));

            db.OutboxMessages.Add(outboxMsg);
            db.Employees.Add(employee);
            await db.SaveChangesAsync();
            
            return TypedResults.Ok(new EmployeeCreatedResponse(employee.Name,employee.Email));
        });
    }
}

public sealed record EmployeeCreatedRequest(string Name, string Email, decimal Salary);
public sealed record EmployeeCreatedResponse(string Name,string Email);