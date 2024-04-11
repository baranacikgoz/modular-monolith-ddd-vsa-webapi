using System.ComponentModel.DataAnnotations;

namespace Common.Options;

public class EventBusOptions
{
    [Required]
    public bool UseInMemoryEventBus { get; set; }
    public MessageBrokerOptions? MessageBrokerOptions { get; set; }
}

public class MessageBrokerOptions
{
    [Required, AllowedValues(nameof(MessageBrokerType.RabbitMQ), nameof(MessageBrokerType.Kafka), nameof(MessageBrokerType.AzureServiceBus), nameof(MessageBrokerType.AmazonSQS))]
    public MessageBrokerType MessageBrokerType { get; set; }

    [Required(AllowEmptyStrings = false)]
    public Uri Uri { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Username { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; } = null!;
}

public enum MessageBrokerType
{
    RabbitMQ,
    Kafka,
    AzureServiceBus,
    AmazonSQS
}
