using System;
using System.Net;
using System.Net.Sockets;
class TestSocket
{
	public static void Main()
	{
		IPAddress ip=IPAddress.Parse("127.0.0.1");
		IPEndPoint iep=new IPEndPoint(ip,8000);
		Socket socket=new Socket(AddressFamily.InterNetwork,
			SocketType.Stream,ProtocolType.Tcp);
		Console.WriteLine("Blocking: {0}",socket.Blocking);
		Console.WriteLine("Connected: {0}",socket.Connected);
		socket.Bind(iep);
		Console.WriteLine("Local EndPoint: {0}",socket.LocalEndPoint.ToString());
		Console.Read();
	}
}
