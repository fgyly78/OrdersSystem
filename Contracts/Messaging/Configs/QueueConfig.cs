namespace Contracts.Messaging.Configs;

public static class QueueConfig
{
    public const bool Durable = true;
    public const bool Exclusive = false;
    public const bool AutoDelete = false;
}