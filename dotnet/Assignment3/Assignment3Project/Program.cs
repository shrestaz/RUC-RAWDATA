using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Console = System.Console;

namespace Assignment3Project
{
    class Program
    {
        public static T FromJson<T>(string element)
        {
            return JsonSerializer.Deserialize<T>(element, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Server is starting");
            var server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
            server.Start();

            while (true)
            {
                Console.WriteLine("Server started. Waiting for connection...");
                var client = server.AcceptTcpClient();
                Console.WriteLine("Connected.");

                var stream = client.GetStream();
                var buffer = new byte[client.ReceiveBufferSize];
                var readDataFromStream = stream.Read(buffer, 0, buffer.Length);
                var msg = Encoding.UTF8.GetString(buffer, 0, readDataFromStream);
                var parsedMsg = FromJson<object>(msg);


                Console.WriteLine($"Message received: {parsedMsg}");
                buffer = Encoding.UTF8.GetBytes(msg);
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
            }
        }
    }
}
