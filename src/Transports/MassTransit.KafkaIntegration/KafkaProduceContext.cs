namespace MassTransit.KafkaIntegration
{
    using Confluent.Kafka;


    public interface KafkaProduceContext :
        SendContext
    {
        Partition Partition { get; set; }
    }


    public interface KafkaProduceContext<out T> :
        SendContext<T>,
        KafkaProduceContext
        where T : class
    {
    }
}
