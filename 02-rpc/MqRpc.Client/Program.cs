using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace MqRpc.Client
{
    class Program
    {
        private static IBusControl _bus;

        static async Task Main(string[] args)
        {
            var a = 2;
            var b = 3;

            _bus = Bus.Factory.CreateUsingRabbitMq();
            await _bus.StartAsync();

            var response = await _bus.Request<SumRequest, MathResponse>(new SumRequest(){ A = a, B= b});
            
            Console.WriteLine(response.Message.Result);

            await _bus.StopAsync();

        }
    }
}