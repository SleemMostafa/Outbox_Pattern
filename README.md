# Outbox Pattern for Email Sending with User Registration

This project implements the **Outbox Pattern** for sending emails in a transactional, reliable way using the `OutboxMessage` table. The pattern is applied to a user registration flow where an email is sent after a successful registration.

## Overview

The Outbox Pattern ensures that emails (or other messages) are reliably sent even if the system experiences failures. Instead of sending an email immediately when a user registers, the email details are first stored in an outbox (represented by the `OutboxMessage` table). A background worker later processes the messages in the outbox and sends the emails.

## Key Components

1. **OutboxMessage Class**: Represents the email message to be processed and sent.
2. **User Registration Endpoint**: When a user registers, an email is queued in the outbox as part of the transaction.
3. **Background Processor**: Periodically checks the outbox table for unsent messages and processes them.

## OutboxMessage Class

The `OutboxMessage` class captures the essential details of an email or event that needs to be processed later. Below is the class structure:

```csharp
public class OutboxMessage
{
    public required string Id { get; init; }
    public required DateTime Created { get; init; }
    public required string Type { get; init; }
    public required string Data { get; init; }
    public bool IsProcessed { get; private set; }
    public DateTimeOffset ProcessedDate { get; private set; }

    public void MarkAsProcessed(DateTime now)
    {
        IsProcessed = true;
        ProcessedDate = now;
    }

    public static OutboxMessage CreateInstance(string type, string data)
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
