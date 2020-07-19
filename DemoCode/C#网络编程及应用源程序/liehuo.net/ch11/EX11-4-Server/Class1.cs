using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace TestUdpServer
{
	class Class1
	{
		static void Main()
		{
			StartListener();
			Console.ReadLine();
		}
		//      
		private static void StartListener()
		{
			UdpClient server=new UdpClient(8080);
			IPEndPoint myhost=null; 
			try
			{
				while(true)
				{
					Console.WriteLine("等待接收...");
					byte[] bytes=server.Receive(ref myhost);
					string str=Encoding.Unicode.GetString(bytes,0,bytes.Length);
					Console.WriteLine("接收到信息：{0}",str);
					//如果收到的消息是"byebye"，则跳出给循环
					if(str=="byebye") break;
					//以下三句用来向客户端回送消息
					Console.WriteLine("发送应答信息：你好，我也爱你！");
					bytes=Encoding.Unicode.GetBytes("你好，我也爱你！");
					server.Send(bytes,bytes.Length,"127.0.0.1",8081);
				}
				server.Close();
				Console.WriteLine("对方已经byebye了，请按回车键退出。");
			}
			catch(Exception err)
			{
				Console.WriteLine(err.ToString());
			}
		}
	}
}
