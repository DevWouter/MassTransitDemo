using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace MqDemo.FastSpeed
{
    public class FastSpeedConsumer : IConsumer<FastSpeedMessage>
    {
        public Task Consume(ConsumeContext<FastSpeedMessage> context)
        {
            Console.WriteLine($"FastIndex: {context.Message.TypedIndex} -- Index: {context.Message.Index}");
            
            Thread.Sleep(10);

            return Task.CompletedTask;
        }
    }
}