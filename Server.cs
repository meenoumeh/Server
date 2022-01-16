using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class Server
    {
        public IPAddress MyIP { get; private set; }
        public int Port { get; private set; }
        public bool Status = true;
        public TcpListener TcpListener { get; set; }
        public Socket SocketForClient { get; set; }

        public NetworkStream NetworkStream { get; set; }
        public StreamReader StreamReader { get; set; }
        public StreamWriter StreamWriter { get; set; }

        public Server(IPAddress iPAddress, int port)
        {
            MyIP = iPAddress;
            Port = port;
        }

        public void StartListenning()
        {
            try
            {
                TcpListener = new TcpListener(MyIP, Port);

            }
            catch (Exception ex)
            {
                Console.WriteLine("can not start ....");
            }
        }

        public void AcceptClient()
        {
            try
            {
                SocketForClient = TcpListener.AcceptSocket();
            }
            catch (Exception ex)
            {
                Console.WriteLine("can not accept client ...");
            }
        }

        public void ClientData()
        {
            NetworkStream = new NetworkStream(SocketForClient);
            StreamReader = new StreamReader(NetworkStream);
            StreamWriter = new StreamWriter(NetworkStream);

        }

        public void Disconect()
        {
            NetworkStream.Close();
            StreamWriter.Close();
            StreamReader.Close();
            SocketForClient.Close();
        }

    }
}
