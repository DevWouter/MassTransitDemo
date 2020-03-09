using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace MqDemo.SlowSpeed
{
    public class SlowSpeedConsumer : IConsumer<SlowSpeedMessage>
    {
        public Task Consume(ConsumeContext<SlowSpeedMessage> context)
        {
            Console.WriteLine($"START  SlowIndex: {context.Message.TypedIndex} -- Index: {context.Message.Index}");
            
            Thread.Sleep(1000);
            
            Console.WriteLine($"FINISH SlowIndex: {context.Message.TypedIndex} -- Index: {context.Message.Index}");

            return Task.CompletedTask;
        }
    }
}