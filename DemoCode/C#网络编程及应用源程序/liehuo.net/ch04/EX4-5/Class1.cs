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
		Console.WriteLine("����AddOne֮ǰ��a={0}",a);
		AddOne(a);
		Console.WriteLine("����AddOne֮��a={0}",a);
		Console.Read();
	}
}
