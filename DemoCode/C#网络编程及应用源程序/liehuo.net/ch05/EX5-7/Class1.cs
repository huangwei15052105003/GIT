using System;
namespace Version2
{
	class A
	{
		public virtual void Method()
		{
			Console.WriteLine("A.Method");
		}
	}
	class B:A
	{
		public new virtual void Method()
		{
			Console.WriteLine("B.Method");
		}
	}
	class VersionControl
	{
		public static void Main() 
		{
			A a=new A();
			B b=new B();
			A c=b;
			a.Method();
			b.Method();
			c.Method();
			Console.Read();
		}
	}
}
