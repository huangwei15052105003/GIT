using System;
using System.Text;
namespace ConsoleTest
{
	class TestStringBuild
	{
		public static void Main()
		{
			StringBuilder str=new StringBuilder();
			Console.WriteLine("字符串是：“{0}”，长度：{1}",str,str.Length);
			Console.WriteLine("内存容量分配：{0}",str.Capacity);
			str=new StringBuilder("test string.");
			Console.WriteLine("字符串是：“{0}”，长度：{1}",str,str.Length);
			Console.WriteLine("内存容量分配：{0}",str.Capacity);
			str.Append("append another string.");
			Console.WriteLine("字符串是：“{0}”，长度：{1}",str,str.Length);
			Console.WriteLine("内存容量分配：{0}",str.Capacity);
			str=new StringBuilder("test string.",5);
			Console.WriteLine("字符串是：“{0}”，长度：{1}",str,str.Length);
			Console.WriteLine("内存容量分配：{0}",str.Capacity);
			str=new StringBuilder("test string.",40);
			Console.WriteLine("字符串是：“{0}”，长度：{1}",str,str.Length);
			Console.WriteLine("内存容量分配：{0}",str.Capacity);
			Console.ReadLine();
		}
	}
}
