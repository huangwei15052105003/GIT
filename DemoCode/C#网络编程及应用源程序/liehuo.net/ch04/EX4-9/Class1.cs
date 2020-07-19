using System;
class Method
{
	public static int Add(int i,int j)
	{
		return i+j;
	}
	public static string Add(string s1,string s2)
	{
		return s1+s2;
	}
	public static long Add(long x)
	{
		return x+5;
	}
	static void Main()
	{
		Console.WriteLine(Add(1,2));
		Console.WriteLine(Add("1","2"));
		Console.WriteLine(Add(10));
		Console.Read();
	}
}
