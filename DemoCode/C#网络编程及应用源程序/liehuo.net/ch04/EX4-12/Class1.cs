using System;
using System.Text;
namespace ConsoleTest
{
	class TestStringBuild
	{
		public static void Main()
		{
			StringBuilder str=new StringBuilder();
			Console.WriteLine("�ַ����ǣ���{0}�������ȣ�{1}",str,str.Length);
			Console.WriteLine("�ڴ��������䣺{0}",str.Capacity);
			str=new StringBuilder("test string.");
			Console.WriteLine("�ַ����ǣ���{0}�������ȣ�{1}",str,str.Length);
			Console.WriteLine("�ڴ��������䣺{0}",str.Capacity);
			str.Append("append another string.");
			Console.WriteLine("�ַ����ǣ���{0}�������ȣ�{1}",str,str.Length);
			Console.WriteLine("�ڴ��������䣺{0}",str.Capacity);
			str=new StringBuilder("test string.",5);
			Console.WriteLine("�ַ����ǣ���{0}�������ȣ�{1}",str,str.Length);
			Console.WriteLine("�ڴ��������䣺{0}",str.Capacity);
			str=new StringBuilder("test string.",40);
			Console.WriteLine("�ַ����ǣ���{0}�������ȣ�{1}",str,str.Length);
			Console.WriteLine("�ڴ��������䣺{0}",str.Capacity);
			Console.ReadLine();
		}
	}
}
