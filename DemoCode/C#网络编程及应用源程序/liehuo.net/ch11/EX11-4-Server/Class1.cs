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
					Console.WriteLine("�ȴ�����...");
					byte[] bytes=server.Receive(ref myhost);
					string str=Encoding.Unicode.GetString(bytes,0,bytes.Length);
					Console.WriteLine("���յ���Ϣ��{0}",str);
					//����յ�����Ϣ��"byebye"����������ѭ��
					if(str=="byebye") break;
					//��������������ͻ��˻�����Ϣ
					Console.WriteLine("����Ӧ����Ϣ����ã���Ҳ���㣡");
					bytes=Encoding.Unicode.GetBytes("��ã���Ҳ���㣡");
					server.Send(bytes,bytes.Length,"127.0.0.1",8081);
				}
				server.Close();
				Console.WriteLine("�Է��Ѿ�byebye�ˣ��밴�س����˳���");
			}
			catch(Exception err)
			{
				Console.WriteLine(err.ToString());
			}
		}
	}
}
