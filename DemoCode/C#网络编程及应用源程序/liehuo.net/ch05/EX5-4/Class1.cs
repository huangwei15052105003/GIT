using System;
public class Hello
{
	public void SayHello()
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
		Hello b=new Hello();
		b.SayHello();
		NewHello d=new NewHello();
		d.SayHello();
		Console.Read();
	}
}
