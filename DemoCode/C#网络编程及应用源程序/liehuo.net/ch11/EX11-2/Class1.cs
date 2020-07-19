using System;
using System.Net;
using System.Net.Sockets;
class TestSimpleUdp
{
	public static void Main()
	{
		int length;
		byte[] bytes=new byte[1024];
		Socket socket=
			new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
		//参数1指定本机IP地址（此处指所有可用的IP地址），参数2指定接收用的端口
		IPEndPoint myHost=new IPEndPoint(IPAddress.Any,6789);
		//将本机IP地址和端口与套接字绑定，为接收做准备
		socket.Bind(myHost);
		//定义远程终端IP地址和端口（实际使用时应为远程主机IP地址），为发
		//送数据做准备
		IPEndPoint remote=new IPEndPoint(IPAddress.Parse("127.0.0.1"),6789);
		//建立与远程主机的连接
		socket.Connect(remote);
		Console.Write("输入发送的信息：");
		string str=Console.ReadLine();
		//字符串转换为字节数组
		bytes=System.Text.Encoding.Unicode.GetBytes(str);
		//向远程终端发送信息
		socket.Send(bytes);
		while(true)
		{
			Console.WriteLine("等待接收...");
			//从本地绑定的IP地址和端口接收远程终端的数据，返回接收的字节数
			length=socket.Receive(bytes);
			//字节数组转换为字符串
			str=System.Text.Encoding.Unicode.GetString(bytes,0,length);
			Console.WriteLine("接收到信息：{0}",str);
			//如果收到的消息是"bye"，则跳出给循环
			if(str=="bye") break;
			Console.Write("输入回送信息（bye退出）：");
			str=Console.ReadLine();
			bytes=System.Text.Encoding.Unicode.GetBytes(str);
			socket.Send(bytes);
		}
		//关闭套接字
		socket.Close();
		Console.WriteLine("对方已经byebye了，请按回车键结束。");
		Console.ReadLine();
	}
}
