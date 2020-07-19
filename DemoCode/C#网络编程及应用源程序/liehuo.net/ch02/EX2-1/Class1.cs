using System;
namespace ConsoleTest
{
	public struct Point
	{
		public int x;
		public int y;
		public string s;
	}
	public struct Point1
	{
		public static int x;
		public static int y;
		public static string s;
	}
	class TestStruct
	{
		public static void Main()
		{
			Point p;
			p.x=3;
			p.y=4;
			p.s="ok";
			Console.WriteLine("The result is {0},{1},{2}",p.x,p.y,p.s);
			Point1.x=1;
			Point1.y=2;
			Point1.s="good";
			Console.WriteLine("The result is {0},{1},{2}",Point1.x,Point1.y,Point1.s);
			Console.Read();
		}
	}
}
