using System;
class A
{
	public void F()	{}
}
class B: A
{
	public void F()	{}
	public static void Main()
	{
		B me=new B();
		me.F();
		Console.Read();
	}
}
