using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;

namespace MqDemo.Generator
{

    public enum MessageType
    {
        Fast, Slow
    }
    
    class Program
    {
        private static int slowMessageCounter = 0;
        private static int fastMessageCounter = 0;
        private static int totalMessageCounter = 0;
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
            });
            await bus.StartAsync();
            
            Console.WriteLine("Instructions");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Press Q to quite");
            Console.WriteLine("Press S to queue a slow message");
            Console.WriteLine("Press F to queue a fast message");
            Console.WriteLine("Press <enter> to send all the queued messages to the processors");
            Console.WriteLine();

            List<MessageType> messages =  new List<MessageType>();

            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Q: break;
                    case ConsoleKey.S:
                    {
                        messages.Add(MessageType.Slow);
                        break;
                    }
                    case ConsoleKey.F:
                    {
                        messages.Add(MessageType.Fast);
                        break;
                    }
                    case ConsoleKey.Enter:
                    {
                        // Send all
                        SendMessages(bus, messages);
                        messages = new List<MessageType>();
                        break;
                    }
                }
            }

            await bus.StopAsync();
        }

        private static void SendMessages(IBus bus, List<MessageType> messages)
        {
            foreach (var messageType in messages)
            {
                switch (messageType)
                {
                    case MessageType.Fast:
                        bus.Publish(new FastSpeedMessage
                        {
                            Index = totalMessageCounter++,
                            TypedIndex = fastMessageCounter++
                        });
                        break;
                    case MessageType.Slow:
                        bus.Publish(new SlowSpeedMessage
                        {
                            Index = totalMessageCounter++,
                            TypedIndex = slowMessageCounter++
                        });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}