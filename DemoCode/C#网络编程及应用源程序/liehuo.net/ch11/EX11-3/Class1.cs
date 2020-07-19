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
		//���ý��ճ�ʱʱ��Ϊ2��
		socket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReceiveTimeout,2000);
		//����1ָ������IP��ַ���˴�ָ���п��õ�IP��ַ��������2ָ�������õĶ˿�
		IPEndPoint myHost=new IPEndPoint(IPAddress.Any,6789);
		//������IP��ַ�Ͷ˿����׽��ְ󶨣�Ϊ������׼��
		socket.Bind(myHost);
		//����Զ���ն˵�IP��ַ�Ͷ˿ڣ�ʵ��ʹ��ʱӦΪԶ������IP��ַ����Ϊ����������׼��
		IPEndPoint iep=new IPEndPoint(IPAddress.Parse("127.0.0.1"),6789);
		EndPoint remote=(EndPoint)iep;
		while(true)
		{
			int retry=0;
			while(true)
			{
				try
				{
					Console.Write("���뷢�͵���Ϣ��bye�˳�����");
					string str=Console.ReadLine();
					if(str=="bye")
					{
						exit=true;
						break;
					}
					//�ַ���ת��Ϊ�ֽ�����
					bytes=System.Text.Encoding.Unicode.GetBytes(str);
					//��Զ���ն˷�����Ϣ
					socket.SendTo(bytes,remote);
					//�ӱ��ذ󶨵�IP��ַ�Ͷ˿ڽ���Զ���ն˵����ݣ����ؽ��յ��ֽ���
					socket.ReceiveFrom(bytes,ref remote);
					str=System.Text.Encoding.Unicode.GetString(bytes);
					Console.WriteLine("���յ���Ϣ��{0}",str);
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
						Console.WriteLine("����ʧ�ܣ�");
						break;
					}
				}
			}
			if(exit) break;
		}
		//�ر��׽���
		socket.Close();
		Console.WriteLine("�밴�س���������");
		Console.ReadLine();
	}
}
