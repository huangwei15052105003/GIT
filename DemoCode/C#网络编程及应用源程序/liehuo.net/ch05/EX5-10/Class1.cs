using System;
namespace TestInterface
{
	interface Ifunction
	{
		int sum(int x1,int x2);
	}
	class MyTest:Ifunction
	{
		//ʵ�ֽӿ�Ifunction1�еķ���
		int Ifunction.sum(int x1,int x2)
		{
			return x1+x2;
		}
	}
	class MainClass
	{
		public static void Main()
		{
			//ֱ�ӷ���ʵ��������ʾ��MyTest��������sum�Ķ��塱�Ĵ���
			//��Ϊsum����ʽʵ�ֽӿڣ�ֻ��ͨ���ӿڵ���
			//MyTest a=new MyTest();
			//Console.WriteLine(a.sum(10,20)); 
			//ͨ���ӿڷ���ʵ��
			Ifunction b=new MyTest();
			Console.WriteLine(b.sum(20,30));
			Console.Read();
		}
	}
}
