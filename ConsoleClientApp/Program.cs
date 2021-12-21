using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ConsoleClientApp
{
    internal class Program
    {
        private static HubConnection _hubConnection;

        private static async Task Main(string[] args)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:8080/chatHub")
                .Build();

            _hubConnection.On<MessageDto>("receiveMessage", result =>
            {
                Console.WriteLine("Username: {0} --> Message: {1}", result.Username, result.Message);
            });

            _hubConnection.Closed += async (error) =>
            {
                await _hubConnection.StartAsync();
                Console.WriteLine(error.Message.ToString());
            };

            try
            {
                await _hubConnection.StartAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            Console.ReadKey();
            //while (true)
            //{
            //    Console.Write("User Name :");
            //    var userName = Console.ReadLine();
            //    Console.Write("Message :");
            //    var sendMessage = Console.ReadLine();

            //    MessageDto messageDto = new MessageDto()
            //    {
            //        ConnectionId = _hubConnection.ConnectionId,
            //        Username = userName,
            //        Message = sendMessage,
            //    };
            //    await _hubConnection.SendAsync("sendMessage", messageDto);

            //    Console.WriteLine("Message Sent.");
            //}
        }
    }
}