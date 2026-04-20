namespace Contracts.Events;

public record OrderCreatedIntegrationEvent(Guid OrderId, DateTime CreatedAt);
