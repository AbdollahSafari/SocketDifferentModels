using System.Net.Sockets;
using System.Net;

namespace SocketDifferentModels;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");


        //****************************************Async Mode*************************************
        //UdpListener udpListener = new UdpListener(IPAddress.Parse("120.110.1.11"), 9090);
        //udpListener.StartListen();
        //udpListener.StartReceive();

        //Console.ReadLine();


        //*******************************************Pooling***********************************
        //var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //socket.Bind(new IPEndPoint(IPAddress.Parse("120.110.1.11"),9090));
        //while (true)
        //{
        //    if (socket.Available > 0)
        //    {
        //        var buffer = new byte[socket.Available];
        //        EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
        //        var readCout = socket.ReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endPoint);
        //        Console.WriteLine($"Data Received From : {endPoint}, {Encoding.ASCII.GetString(buffer, 0, readCout)}");
        //    }
        //}

        //****************************************Async Await*************************************

        UdpListener udpListener = new UdpListener(IPAddress.Parse("120.110.1.11"), 9090);
        udpListener.StartListen();
        udpListener.StartReceiveAsync();
        while (true)
        {
            var line = Console.ReadLine();
            udpListener.SendData(line);
        }
    }
}