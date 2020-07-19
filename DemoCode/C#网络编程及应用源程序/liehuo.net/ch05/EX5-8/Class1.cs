using System;
namespace Version3
{
	class A
	{
		public abstract  void Method()
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
	class C:B
	{
		public override void Method()
		{
			Console.WriteLine("C.Method");
		}
	}
	class VersionControl
	{
		public static void Main() 
		{
			A a=new A();
			B b=new C();
			A c=b;
			a.Method();
			b.Method();
			c.Method();
			Console.Read();
		}
	}
}
