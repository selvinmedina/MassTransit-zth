using MassTransit;

namespace HelloApi.Cotracts
{
    [EntityName("message-submitted")]
    //[ExcludeFromTopology]
    [ConfigureConsumeTopology(false)]
    public class Message
    {
        public string Text { get; set; }
    }
}
