using System;
using System.Net;
using System.Net.Sockets;
namespace TestUdpClient
{
	class Class1
	{
		static void Main(string[] args)
		{
			Send("��ã�����! I love you!");
			Send("byebye");
			Console.ReadLine();
		}
		private static void Send(string message)
		{
			UdpClient client=new UdpClient(8081);
			try
			{
				Console.WriteLine("��8080�������ݣ�{0}",message);
				byte[] bytes=System.Text.Encoding.Unicode.GetBytes(message);
				client.Send(bytes,bytes.Length,"127.0.0.1",8080);
				if(message=="byebye")
				{
					Console.WriteLine("�Ѿ���Է�����byebye�ˣ��밴�س����˳�����");
					return;
				}
				IPEndPoint host=null;
				byte[] response=client.Receive(ref host);
				Console.WriteLine("���յ�������Ϣ��{0}",
					System.Text.Encoding.Unicode.GetString(response));
				client.Close();
			}
			catch(Exception err)
			{
				Console.WriteLine(err.ToString());
			}
		}
	}
}
