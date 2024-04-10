namespace RabbitExample.Rabbit
{
    public interface IRabbitProducer
    {
        void SendProductMessage<T>(T message);

        void SendLogMessage<T>(T message);
    }
}
