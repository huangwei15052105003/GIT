using System;
class A
{
	public int x=1;
	public virtual void PrintFields()
	{
		Console.WriteLine("x = {0}", x);
	}
}
class B: A
{
	int y=2;
	public override void PrintFields()
	{
		base.PrintFields();
		Console.WriteLine(x);
		Console.WriteLine("y = {0}", y);
	}
	public static void Main()
	{
		B me=new B();
		me.PrintFields();
		Console.Read();
	}
}
