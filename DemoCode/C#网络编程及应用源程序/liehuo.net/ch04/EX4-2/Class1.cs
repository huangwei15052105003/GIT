using System;
public class Test
{
	public static int j=20;
	public static void Main()
	{
		int j=30;
		Console.WriteLine(j);      //��������30
		Console.WriteLine(Test.j);  //��������20
		Console.Read();
	}
}
