using System;
using System.Net;
using System.Net.Sockets;
class UdpClientMultiSnd
{
	static void Main()
	{
		UdpClient uc=new UdpClient();
		IPEndPoint iep=new IPEndPoint(IPAddress.Parse("224.100.0.1"),6788);
		byte[] bytes=System.Text.Encoding.Unicode.GetBytes("This is a test Message");
		uc.Send(bytes,bytes.Length,iep);
		uc.Close();
	}
}
