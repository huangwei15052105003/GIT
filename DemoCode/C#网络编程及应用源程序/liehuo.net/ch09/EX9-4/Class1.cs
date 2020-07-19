using System;
using System.Net;
class TestIPEndPoint
{
	public static void Main()
	{
		IPAddress ip=IPAddress.Parse("127.0.0.1");
		IPEndPoint iep=new IPEndPoint(ip,8000);
		Console.WriteLine("The IPEndPoint is:{0}",iep.ToString());
		Console.WriteLine("The Address is:{0}",iep.Address);
		Console.WriteLine("The AddressFamily is:{0}",iep.AddressFamily);
		Console.WriteLine("The max port number is:{0}",IPEndPoint.MaxPort);
		Console.WriteLine("The min port number is:{0}",IPEndPoint.MinPort);
		Console.Read();
	}
}
