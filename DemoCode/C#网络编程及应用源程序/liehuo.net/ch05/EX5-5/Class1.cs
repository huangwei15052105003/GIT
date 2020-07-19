using System;
public class Hello
{
	public sealed void SayHello()
	{
		Console.WriteLine("这是基类!");
	}
}
public class NewHello:Hello
{
	public new void SayHello()
	{
		Console.WriteLine("这是扩充类!");
	}
	public static void Main()
	{
		NewHello me=new NewHello();
		me.SayHello();
	}
}
