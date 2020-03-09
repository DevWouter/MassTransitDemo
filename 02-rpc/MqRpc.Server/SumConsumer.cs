using System.Threading.Tasks;
using MassTransit;

namespace MqRpc.Server
{
    public class SumConsumer:IConsumer<SumRequest>
    {
        public Task Consume(ConsumeContext<SumRequest> context)
        {
            var message = context.Message;
            
            var mathResponse = new MathResponse
            {
                Result = message.A + message.B
            };
            
            return context.RespondAsync(mathResponse);
        }
    }
}