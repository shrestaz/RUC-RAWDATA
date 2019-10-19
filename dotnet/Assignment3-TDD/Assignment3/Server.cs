using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using RDJTPServer.Helpers;

namespace RDJTPServer
{
    public class Server
    {
        public static void CreateServer()
        {
            Console.WriteLine("RDJTP server is starting");
            var server = new TcpListener(IPAddress.Loopback, 5000);
            try
            {
                server.Start();
                while (true)
                {
                    Console.WriteLine("Server started. Waiting for connection...");
                    var client = server.AcceptTcpClient();
                    Console.WriteLine("Connected.");

                    var request = client.ReadRequest();
                    var response = new Validation().ValidateRequest(request);
                    var responseToSend = response.ToJson();
                    client.SendResponse(responseToSend);
                }

            }
            catch (SocketException e)
            {
                server.Start();
                Console.WriteLine(e);
                throw;
            }
        }
    }

}