using System;
using System.IO;
public class TestFileStream
{
	static void Main()
	{
		StreamWriter sw = new StreamWriter("MyFile.txt",true,System.Text.Encoding.Unicode);
		sw.WriteLine("第一条语句。");
		sw.WriteLine("第二条语句。");
		//关闭当前的StreamWriter和基础流
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
