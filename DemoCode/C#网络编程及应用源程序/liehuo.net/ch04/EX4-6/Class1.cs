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
		Console.WriteLine("����AddOne֮ǰ��x={0}",x);
		AddOne(ref x);
		Console.WriteLine("����AddOne֮��x={0}",x);
		Console.Read();
	}
}
