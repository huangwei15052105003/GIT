using System;
class Test
{
	public Test()
	{
		Console.WriteLine("null");
	}
	public Test(string str)
	{
		Console.WriteLine(str);
	}
	static void Main()
	{
		Test aa=new Test();
		Test bb=new Test("How are you!");
		Console.Read();
	}
}
