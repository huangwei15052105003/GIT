using System;
namespace TestInterface
{
	interface Ifunction1
	{
		int sum(int x1,int x2);
	}
	interface Ifunction2
	{
		string str{get;set;}
	}
	class MyTest:Ifunction1,Ifunction2     //此处的冒号表示接口的实现
	{
		private string mystr;
		//构造函数
		public MyTest()
		{}
		//构造函数
		public MyTest(string str)
		{
			mystr=str;
		}
		//实现接口Ifunction1中的方法
		public int sum(int x1,int x2)
		{
			return x1+x2;
		}
		//实现接口Ifunction2中的属性
		public string str
		{
			get
			{
				return mystr;
			}
			set
			{
				mystr=value;
			}
		}
	}
	class MainClass
	{
		public static void Main()
		{
			//直接访问实例
			MyTest a=new MyTest();
			Console.WriteLine(a.sum(10,20));
			MyTest b=new MyTest("How are you");
			Console.WriteLine(b.str);
			//通过接口访问实例
			Ifunction1 c=new MyTest();
			Console.WriteLine(c.sum(20,30));
			Ifunction2 d=new MyTest("This is a book!");
			Console.WriteLine(d.str);
			Console.Read();
		}
	}
}
