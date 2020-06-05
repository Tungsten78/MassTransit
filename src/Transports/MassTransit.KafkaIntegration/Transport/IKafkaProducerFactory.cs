namespace MassTransit.KafkaIntegration.Transport
{
    using System;


    public interface IKafkaProducerFactory :
        IDisposable
    {
        string TopicName { get; }
    }
}
