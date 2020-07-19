using System;
using System.Net;
using System.Net.Sockets;
class TestSimpleUdp
{
	public static void Main()
	{
		bool exit=false;
		int length;
		byte[] bytes=new byte[1024];
		Socket socket=new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
		//设置接收超时时间为2秒
		socket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReceiveTimeout,2000);
		//参数1指定本机IP地址（此处指所有可用的IP地址），参数2指定接收用的端口
		IPEndPoint myHost=new IPEndPoint(IPAddress.Any,6789);
		//将本机IP地址和端口与套接字绑定，为接收做准备
		socket.Bind(myHost);
		//定义远程终端的IP地址和端口（实际使用时应为远程主机IP地址），为发送数据做准备
		IPEndPoint iep=new IPEndPoint(IPAddress.Parse("127.0.0.1"),6789);
		EndPoint remote=(EndPoint)iep;
		while(true)
		{
			int retry=0;
			while(true)
			{
				try
				{
					Console.Write("输入发送的信息（bye退出）：");
					string str=Console.ReadLine();
					if(str=="bye")
					{
						exit=true;
						break;
					}
					//字符串转换为字节数组
					bytes=System.Text.Encoding.Unicode.GetBytes(str);
					//向远程终端发送信息
					socket.SendTo(bytes,remote);
					//从本地绑定的IP地址和端口接收远程终端的数据，返回接收的字节数
					socket.ReceiveFrom(bytes,ref remote);
					str=System.Text.Encoding.Unicode.GetString(bytes);
					Console.WriteLine("接收到信息：{0}",str);
				}
				catch
				{
					if(retry<3)
					{
						retry++;
						continue;
					}
					else
					{
						Console.WriteLine("发送失败！");
						break;
					}
				}
			}
			if(exit) break;
		}
		//关闭套接字
		socket.Close();
		Console.WriteLine("请按回车键结束。");
		Console.ReadLine();
	}
}
