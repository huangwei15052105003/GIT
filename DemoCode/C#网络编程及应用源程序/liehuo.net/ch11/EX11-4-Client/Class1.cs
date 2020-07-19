using System;
using System.Net;
using System.Net.Sockets;
namespace TestUdpClient
{
	class Class1
	{
		static void Main(string[] args)
		{
			Send("你好，朋友! I love you!");
			Send("byebye");
			Console.ReadLine();
		}
		private static void Send(string message)
		{
			UdpClient client=new UdpClient(8081);
			try
			{
				Console.WriteLine("向8080发送数据：{0}",message);
				byte[] bytes=System.Text.Encoding.Unicode.GetBytes(message);
				client.Send(bytes,bytes.Length,"127.0.0.1",8080);
				if(message=="byebye")
				{
					Console.WriteLine("已经向对方发送byebye了，请按回车键退出程序");
					return;
				}
				IPEndPoint host=null;
				byte[] response=client.Receive(ref host);
				Console.WriteLine("接收到返回信息：{0}",
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
