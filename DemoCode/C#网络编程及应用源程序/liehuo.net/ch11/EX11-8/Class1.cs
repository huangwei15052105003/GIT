using System;
using System.Net;
using System.Net.Sockets;
class UdpClientMultiRecv
{
	public static void Main()
	{
		UdpClient uc=new UdpClient(6788);
		uc.JoinMulticastGroup(IPAddress.Parse("224.100.0.1"),50);
		IPEndPoint iep=new IPEndPoint(IPAddress.Any,0);
		byte[] bytes=uc.Receive(ref iep);
		string str=System.Text.Encoding.Unicode.GetString(bytes,0,bytes.Length);
		Console.WriteLine(str);
		uc.Close();
	}
}
