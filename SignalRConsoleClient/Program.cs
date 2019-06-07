using System;
using ConsoleClient;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace SignalRConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press a key to start listening..");
            Console.ReadKey();
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44362/coffeehub")
                .AddMessagePackProtocol()
                .Build();

            connection.On<Order>("NewOrder", (order) =>
                Console.WriteLine($"Somebody ordered an {order.Product}"));

            connection.On<string>("ReceiveOrderUpdate", (update) =>
               Console.WriteLine($"Status: {update}"));

            connection.StartAsync().GetAwaiter().GetResult();

            Console.WriteLine("Listening. Press a key to quit");
            Console.ReadKey();
        }
    }
}
