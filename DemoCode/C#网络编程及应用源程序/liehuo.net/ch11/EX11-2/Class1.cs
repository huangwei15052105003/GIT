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
		//����1ָ������IP��ַ���˴�ָ���п��õ�IP��ַ��������2ָ�������õĶ˿�
		IPEndPoint myHost=new IPEndPoint(IPAddress.Any,6789);
		//������IP��ַ�Ͷ˿����׽��ְ󶨣�Ϊ������׼��
		socket.Bind(myHost);
		//����Զ���ն�IP��ַ�Ͷ˿ڣ�ʵ��ʹ��ʱӦΪԶ������IP��ַ����Ϊ��
		//��������׼��
		IPEndPoint remote=new IPEndPoint(IPAddress.Parse("127.0.0.1"),6789);
		//������Զ������������
		socket.Connect(remote);
		Console.Write("���뷢�͵���Ϣ��");
		string str=Console.ReadLine();
		//�ַ���ת��Ϊ�ֽ�����
		bytes=System.Text.Encoding.Unicode.GetBytes(str);
		//��Զ���ն˷�����Ϣ
		socket.Send(bytes);
		while(true)
		{
			Console.WriteLine("�ȴ�����...");
			//�ӱ��ذ󶨵�IP��ַ�Ͷ˿ڽ���Զ���ն˵����ݣ����ؽ��յ��ֽ���
			length=socket.Receive(bytes);
			//�ֽ�����ת��Ϊ�ַ���
			str=System.Text.Encoding.Unicode.GetString(bytes,0,length);
			Console.WriteLine("���յ���Ϣ��{0}",str);
			//����յ�����Ϣ��"bye"����������ѭ��
			if(str=="bye") break;
			Console.Write("���������Ϣ��bye�˳�����");
			str=Console.ReadLine();
			bytes=System.Text.Encoding.Unicode.GetBytes(str);
			socket.Send(bytes);
		}
		//�ر��׽���
		socket.Close();
		Console.WriteLine("�Է��Ѿ�byebye�ˣ��밴�س���������");
		Console.ReadLine();
	}
}
