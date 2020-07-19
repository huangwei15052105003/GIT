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
	class MyTest:Ifunction1,Ifunction2     //�˴���ð�ű�ʾ�ӿڵ�ʵ��
	{
		private string mystr;
		//���캯��
		public MyTest()
		{}
		//���캯��
		public MyTest(string str)
		{
			mystr=str;
		}
		//ʵ�ֽӿ�Ifunction1�еķ���
		public int sum(int x1,int x2)
		{
			return x1+x2;
		}
		//ʵ�ֽӿ�Ifunction2�е�����
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
			//ֱ�ӷ���ʵ��
			MyTest a=new MyTest();
			Console.WriteLine(a.sum(10,20));
			MyTest b=new MyTest("How are you");
			Console.WriteLine(b.str);
			//ͨ���ӿڷ���ʵ��
			Ifunction1 c=new MyTest();
			Console.WriteLine(c.sum(20,30));
			Ifunction2 d=new MyTest("This is a book!");
			Console.WriteLine(d.str);
			Console.Read();
		}
	}
}
