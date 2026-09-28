namespace FootballBuddy.Auth.Infrastructure.Persistence.Outbox;

public sealed class OutboxMessage
{
    public Guid Id { get; private set; }
    public string Type { get; private set; } = null!;
    public string Payload { get; private set; } = null!;
    public DateTime OccurredOn { get; private set; }
    public DateTime? ProcessedOn { get; private set; }
    public int Attempts { get; private set; }
    public string? LastError { get; private set; }
    
    private OutboxMessage() { }
    
    public OutboxMessage(
        Guid id,
        string type,
        string payload,
        DateTime occurredOn)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Event ID is required.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(type))
        {
            throw new ArgumentException("Event type is required.", nameof(type));
        }

        if (string.IsNullOrWhiteSpace(payload))
        {
            throw new ArgumentException("Payload is required.", nameof(payload));
        }

        Id = id;
        Type = type;
        Payload = payload;
        OccurredOn = occurredOn;
    }

    public void MarkAsProcessed(DateTime processedOn)
    {
        Attempts++;
        ProcessedOn = processedOn;
        LastError = null;
    }

    public void MarkAsFailed(string error)
    {
        Attempts++;
        LastError = error;
    }
    
}