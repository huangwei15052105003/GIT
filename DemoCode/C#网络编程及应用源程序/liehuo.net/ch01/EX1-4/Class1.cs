//EX1-4
using System;
namespace ConsoleTest
{
	class HelloName
	{
		private string s="张三";
		private int i=5;
		public HelloName()
		{
			Console.WriteLine("hello {0},I have {1} books!",s,i);
		}
		static void Main(string[] args)
		{
			int i=1;
			Console.WriteLine("The first is:{0}",i);
			Console.Write("Please enter your name: ");
			string strName = Console.ReadLine();
			Console.WriteLine("Hello " + strName+"!");
			HelloName hello=new HelloName();
			hello.i=6;
			Console.WriteLine("hi {0},I have {1} books!",hello.s,hello.i);
			HelloMe me=new HelloMe();
			me.Welcome();
			Console.Read();
		}
	}
	class HelloMe
	{
		private string s="王五";
		private int i=10;
		public HelloMe()
		{
			Console.WriteLine("hello {0},I have {1} books!",s,i);
		}
		public void Welcome()
		{
			i=11;
			Console.WriteLine("hi I have {0} books,Welcome to using C#.NET!",i);
		}
	}
}
