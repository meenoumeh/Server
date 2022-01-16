using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class Program
    {
       
        static void Main(string[] args)
        {
            Console.Title = "Server";
            IPAddress ip = IPAddress.Parse("192.168.1.70");
            int port = 3000;
            Server server = new Server(ip, port);

            server.StartListenning();
            Console.WriteLine("server stareted successfully ...");

            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("waiting for connection ...");

            server.AcceptClient();
            Console.WriteLine("client connected ....");


            string messageFromClient = "";
            string messageToClient = "";


            try
            {
                server.ClientData();
                while (server.Status)
                {

                    if (server.SocketForClient.Connected)
                    {
                        messageFromClient = server.StreamReader.ReadLine();
                        Console.WriteLine("client : " + messageFromClient);
                        if (messageFromClient == "exit")
                        {
                            server.Status = false;
                            server.StreamReader.Close();
                            server.StreamWriter.Close();
                            server.NetworkStream.Close();
                            return;
                        }

                        Console.WriteLine("server : ");
                        messageToClient = Console.ReadLine();
                        server.StreamWriter.WriteLine(messageToClient);
                        server.StreamWriter.Flush();
                    }
                }
                server.Disconect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
