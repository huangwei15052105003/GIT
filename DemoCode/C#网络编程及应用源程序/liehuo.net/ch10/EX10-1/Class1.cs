using System;
using System.Net;
public class TestFileStream
{
	static void Main()
	{
		Console.Write("��������������IP��ַ��");
		string str=Console.ReadLine();
		IPHostEntry host=Dns.Resolve(str);
		for(int i=0;i<host.AddressList.Length;i++)
		{
			Console.WriteLine(host.AddressList[i].ToString());
			Console.WriteLine(host.HostName);
		}
		Console.ReadLine();
	}
}
