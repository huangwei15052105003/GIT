using System;
class Method
{
	public static void AddOne(ref int a)
	{
		a++;
	}
	static void Main()
	{
		int x=3;
		Console.WriteLine("调用AddOne之前，x={0}",x);
		AddOne(ref x);
		Console.WriteLine("调用AddOne之后，x={0}",x);
		Console.Read();
	}
}
