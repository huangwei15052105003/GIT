using System;
using System.IO;
public class TestFileStream
{
	static void Main()
	{
		StreamWriter sw = new StreamWriter("MyFile.txt",true,System.Text.Encoding.Unicode);
		sw.WriteLine("��һ����䡣");
		sw.WriteLine("�ڶ�����䡣");
		//�رյ�ǰ��StreamWriter�ͻ�����
		sw.Close();
		StreamReader sr=new StreamReader("MyFile.txt",System.Text.Encoding.Unicode);
		string str;
		while((str=sr.ReadLine())!=null)
		{
			Console.WriteLine(str);
		}
		sr.Close();
		Console.ReadLine();
	}
}
