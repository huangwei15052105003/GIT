using System;
class Method
{
	public static void MyMethod(out int a,out int b)
	{
		a=5;
		b=6;
	}
	static void Main()
	{
		int x,y;
		MyMethod(out x,out y);
		Console.WriteLine("����MyMethod֮��x={0},y={1}",x,y);
		Console.Read();
	}
}
