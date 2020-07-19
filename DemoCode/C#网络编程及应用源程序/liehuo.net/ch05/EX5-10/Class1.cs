using System;
namespace TestInterface
{
	interface Ifunction
	{
		int sum(int x1,int x2);
	}
	class MyTest:Ifunction
	{
		//实现接口Ifunction1中的方法
		int Ifunction.sum(int x1,int x2)
		{
			return x1+x2;
		}
	}
	class MainClass
	{
		public static void Main()
		{
			//直接访问实例，会提示“MyTest不包含对sum的定义”的错误
			//因为sum是显式实现接口，只能通过接口调用
			//MyTest a=new MyTest();
			//Console.WriteLine(a.sum(10,20)); 
			//通过接口访问实例
			Ifunction b=new MyTest();
			Console.WriteLine(b.sum(20,30));
			Console.Read();
		}
	}
}
