using System;
using System.Threading;
class TestThread
{
	public static void Main()
	{
		Thread thread1=new Thread(new ThreadStart(Method1));
		Thread thread2=new Thread(new ThreadStart(Method2));
		thread1.Start();
		thread2.Start();
		Console.Read();
	}
	public static void Method1()
	{
		for (int i=0;i<1000;i++)
			Console.Write("a");
	}
	public static void Method2()
	{
		for (int i=0;i<1000;i++)
			Console.Write("b");
	}
}
