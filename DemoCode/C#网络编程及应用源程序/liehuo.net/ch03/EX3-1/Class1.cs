using System;
public class Test31
{
	public static void Main()
	{
		Console.Write("������ɼ���");
		//�Ӽ��̽���һ���ַ�
		string str=Console.ReadLine();
		//ת��Ϊ����
		int i=Int32.Parse(str.Trim());
		if(i>100 || i<0)
		{
			Console.WriteLine("�ɼ�����0-100��Χ��");
		}
		else
		{
			switch(i/10)
			{
				case 10:
					Console.Write("���֣�");
					goto case 9;
				case 9:
					Console.WriteLine("����");
					break;
				case 8:
				case 7:
					Console.WriteLine("����");
					break;
				case 6:
					Console.WriteLine("����");
					break;
				default:
					Console.WriteLine("������");
					break;
			}
		}
		Console.Read();
	}
}
