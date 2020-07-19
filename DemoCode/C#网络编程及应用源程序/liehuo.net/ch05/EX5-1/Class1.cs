using System;
public class Test
{
	public Test()
	{
		Console.WriteLine("hello,Test.");
	}
}
public class Test1:Test
{
	public Test1()
	{
		Console.WriteLine("hello,Test1.");
	}
	public static void Main()
	{
		Test1 t=new Test1();
		Console.ReadLine();
	}
}
