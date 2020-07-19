using System;
namespace test
{ 
	//第一步：声明委托
	public delegate string MyDelegate(string name);
	public class Test
	{
		//第二步：定义被调用的方法
		public static string FunctionA(string name) 
		{
			return "A say Hello to "+name; 
		}
		public static string FunctionB(string name) 
		{
			return "B say Hello to "+name; 
		}
		//第三步：定义delegate类型的处理函数，并在此函数中
		//         通过delegate类型调用步骤2定义的方法
		public static void MethodA(MyDelegate Me)
		{
			Console.WriteLine(Me("张三")); 
		}
		public static int Main( )
		{
			//第四步：创建实例，传入准备调用的方法名
			MyDelegate a=new MyDelegate(FunctionA); 
			MyDelegate b=new MyDelegate(FunctionB); 
			MethodA(a); 
			MethodA(b);
			Console.Read(); 
			return 0; 
		}
	}
}
