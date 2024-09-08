using MassTransit;

namespace HelloApi.Cotracts
{
    [EntityName("message-submitted")]
    //[ExcludeFromTopology]   
    public class Message
    {
        public string Text { get; set; }
    }
}
