namespace Outbox_Pattern.Data.Entities;

public class OutboxMessage
{
    public required string Id { get; init; }
    public required DateTime Created { get; init; }
    public required string Type { get; init; }
    public required string Data { get; init; }
    public  bool IsProcessed { get; private set; }
    public  DateTimeOffset ProcessedDate { get; private set; }
    
    public void MarkAsProcessed(DateTime now)
    {
        IsProcessed = true;
        ProcessedDate = now;
    }

    public static OutboxMessage CreateInstance(string type,string data)
    {
        return new OutboxMessage
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Type = type,
            Data = data,
        };
    }
    
}
