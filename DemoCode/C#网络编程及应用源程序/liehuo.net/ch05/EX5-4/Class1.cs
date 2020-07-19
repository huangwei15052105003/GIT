using System;
public class Hello
{
	public void SayHello()
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
		Hello b=new Hello();
		b.SayHello();
		NewHello d=new NewHello();
		d.SayHello();
		Console.Read();
	}
}
