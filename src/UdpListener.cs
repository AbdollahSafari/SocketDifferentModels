using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketDifferentModels;

public class UdpListener
{
    private readonly IPAddress _ipAddress;
    private readonly int _port;
    private readonly byte[] _buffer = new byte[1000];
    private EndPoint _endPoint = new IPEndPoint(IPAddress.Any, 0);

    public UdpListener(IPAddress ipAddress, int port)
    {
        _ipAddress = ipAddress;
        _port = port;
    }

    private Socket? _socket;
    public void StartListen()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _socket.Bind(new IPEndPoint(_ipAddress, _port));
    }

    public void StartReceive()
    {
        _socket.BeginReceiveFrom(_buffer, 0, _buffer.Length, SocketFlags.None, ref _endPoint, ReceiveCallBack, null);
    }

    private void ReceiveCallBack(IAsyncResult ar)
    {
        if (ar.IsCompleted)
        {
            var receiveCount = _socket.EndReceiveFrom(ar, ref _endPoint);
            Console.WriteLine($"Data Received From : {_endPoint}, {Encoding.ASCII.GetString(_buffer, 0, receiveCount)}");
            StartReceive();
        }
    }

    public async Task StartReceiveAsync()
    {
        while (true)
        {
            var result = await _socket.ReceiveFromAsync(new Memory<byte>(_buffer), _endPoint);
            Console.WriteLine(
                $"Data Received From : {result.RemoteEndPoint}, {Encoding.ASCII.GetString(_buffer, 0, result.ReceivedBytes)}");
        }
    }

    public void SendData(string data)
    {
        _socket.SendTo(Encoding.ASCII.GetBytes(data), SocketFlags.None,
            new IPEndPoint(IPAddress.Parse("120.110.1.12"), 5452));
    }
}