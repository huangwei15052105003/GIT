using System;
using System.Net;
class GetUserIP
{
	public static void Main()
	{
		//�õ�������
		string name=Dns.GetHostName();
		Console.WriteLine("��������{0}",name);
		IPHostEntry me=Dns.GetHostByName(name);
		//�����ӦIP��ַ
		foreach(IPAddress ip in me.AddressList)
		{
			Console.WriteLine("IP��ַ��",ip.ToString());
		}
		IPAddress ip1=IPAddress.Parse("127.0.0.1");
		IPAddress ip2=IPAddress.Loopback;
		IPAddress ip3=IPAddress.Broadcast;
		IPAddress ip4=IPAddress.Any;
		IPAddress ip5=IPAddress.None;
		Console.WriteLine("ip1 Address: {0}",ip1);
		Console.WriteLine("Loopback Address: {0}",ip2);
		Console.WriteLine("Broadcase Address: {0}",ip3);
		Console.WriteLine("Any Address: {0}",ip4);
		Console.WriteLine("None Address: {0}",ip5);
		Console.Read();
	}
}
