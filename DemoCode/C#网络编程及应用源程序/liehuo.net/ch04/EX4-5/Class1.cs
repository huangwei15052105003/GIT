using System;
class Method
{
	public static void AddOne(int a)
	{
		a++;
	}
	static void Main()
	{
		int a=3;
		Console.WriteLine("调用AddOne之前，a={0}",a);
		AddOne(a);
		Console.WriteLine("调用AddOne之后，a={0}",a);
		Console.Read();
	}
}
