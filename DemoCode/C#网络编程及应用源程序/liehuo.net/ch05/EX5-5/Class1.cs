using System;
public class Hello
{
	public sealed void SayHello()
	{
		Console.WriteLine("���ǻ���!");
	}
}
public class NewHello:Hello
{
	public new void SayHello()
	{
		Console.WriteLine("����������!");
	}
	public static void Main()
	{
		NewHello me=new NewHello();
		me.SayHello();
	}
}
