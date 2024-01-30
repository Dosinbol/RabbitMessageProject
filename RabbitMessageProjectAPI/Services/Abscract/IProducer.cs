namespace RabbitMessageProjectAPI.Services.Abscract
{
    public interface IProducer
    {
        public void SendMessage<T>(T message);
    }
}
